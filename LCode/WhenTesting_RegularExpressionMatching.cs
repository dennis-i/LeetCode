using System.Diagnostics;

namespace LCode;

//TODO
public class WhenTesting_RegularExpressionMatching
{
    [Theory]
    [InlineData(false, "aa", "a")]
    [InlineData(false, "aa", "aaa")]
    [InlineData(true, "abc", "abc")]
    [InlineData(true, "abc", ".bc")]
    [InlineData(true, "aa", "a*")]
    [InlineData(true, "ab", ".*")]
    [InlineData(true, "abbbz", ".*")]
    [InlineData(false, "abcd", "ab*")]
    [InlineData(true, "aab", "c*a*b")]
    [InlineData(false, "aaa", "ab*a")]
    public void TestIt(bool expected, string s, string p)
    {
        Assert.Equal(expected, IsMatch(s, p));
    }



    public bool IsMatch(string s, string p)
    {
        const char term = '$';
        s += term;
        p += term;
        var sSpn = s.AsSpan();
        var pSpn = p.AsSpan();
        return IsMatchInternal(sSpn, pSpn);


    }

    private bool IsMatchInternal(ReadOnlySpan<char> sSpn, ReadOnlySpan<char> pSpn)
    {

        while (!pSpn.IsEmpty && !sSpn.IsEmpty)
        {
            switch (pSpn[0])
            {
                case '.':
                    if (pSpn.Length == 3 && pSpn[1] == '*')
                        return true;
                    pSpn = pSpn.Slice(1);
                    sSpn = sSpn.Slice(1);
                    break;
                case '*':
                    pSpn = pSpn.Slice(1);
                    if (IsMatchInternal(sSpn, pSpn.Slice(1)))
                        return true;
                    sSpn = sSpn.Slice(1);
                    break;
                default:
                    if (pSpn[0] == sSpn[0])
                    {
                        pSpn = pSpn.Slice(1);
                        sSpn = sSpn.Slice(1);
                    }
                    else if (pSpn.Length > 1 && pSpn[1] == '*')
                    {
                        pSpn = pSpn.Slice(1);
                        if (IsMatchInternal(sSpn, pSpn.Slice(1)))
                            return true;
                        sSpn = sSpn.Slice(1);

                    }
                    else
                    {
                        return false;
                    }
                    break;
            }
        }

        if (pSpn.IsEmpty && sSpn.IsEmpty)
            return true;

        return sSpn.IsEmpty && pSpn.Length == 1 && pSpn[0] == '*';


    }

    public class Rejex
    {
        const int range = 130;
        private const char EndLine = '$';
        public Rejex(string pattern)
        {


            var span = pattern.AsSpan();

            var failState = CreateState();
            _states.Add(failState);


            for (int i = 0; i < span.Length; i++)
            {
                int n = _states.Count;
                var state = CreateState();
                if (span[i] == EndLine)
                {
                    state[EndLine].Next = n + 1;
                    state[EndLine].Offset = 1;
                    _states.Add(state);
                }
                else if (span[i] == '.')
                {
                    for (int j = 0; j < state.Length; j++)
                    {
                        state[j].Next = n + 1;
                        state[j].Offset = 1;
                    }
                    _states.Add(state);
                }
                else if (span[i] == '*')
                {

                    var last = _states[^1];
                    for (int j = 0; j < last.Length; j++)
                    {
                        if (last[j].Next == n)
                        {
                            last[j].Next = n - 1;
                        }
                        else if (last[j].Next == 0)
                        {
                            last[j].Next = n;
                            last[j].Offset = 0;
                        }
                    }
                }
                else
                {
                    state[span[i]].Next = n + 1;
                    state[span[i]].Offset = 1;
                    _states.Add(state);
                }

            }

            //var successState = CreateState();
            //for (int i = 0; i < successState.Length; i++)
            //{
            //    successState[i].Next = _states.Count + 1;
            //    successState[i].Offset = 0;
            //}

            //_states.Add(successState);
        }


        public bool IsMatch(string s)
        {
            //s += EndLine;
            var span = s.AsSpan();
            int sPtr = 0;
            int smIdx = 1;
            while (smIdx > 0 && smIdx < _states.Count && sPtr < span.Length)
            {
                var element = _states[smIdx];
                var st = element[span[sPtr]];
                smIdx = st.Next;
                sPtr += st.Offset;
            }
            if (smIdx == 0)
                return false;
            if (smIdx < _states.Count)
            {
                var element = _states[smIdx];
                var st = element[EndLine];
                smIdx = st.Next;
            }

            return smIdx >= _states.Count;

        }

        public void Dump()
        {
            for (int i = 0; i < range; ++i)
            {
                Debug.Write($"{(char)i}  ");
                for (int r = 0; r < _states.Count; r++)
                {
                    Debug.Write($"{_states[r][i]} ");
                }
                Debug.WriteLine("");
            }
        }
        private State[] CreateState()
        {
            var res = new State[range];
            return res;
        }
        private List<State[]> _states = new();


        private struct State
        {
            public int Next { get; set; }
            public int Offset { get; set; }
            public override string ToString() => $"({Next},{Offset})";

        }

    }

}