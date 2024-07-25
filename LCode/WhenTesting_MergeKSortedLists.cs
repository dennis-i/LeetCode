namespace LCode;

public class WhenTesting_MergeKSortedLists
{

    [Theory]
    [InlineData(new[] { 1, 1, 2, 3, 4, 4, 5, 6 }, new[] { 1, 4, 5 }, new[] { 1, 3, 4 }, new[] { 2, 6 })]
    [InlineData(new int[0], new int[0], new int[0], new int[0])]
    [InlineData(new int[0], null, null, null)]
    public void TestIt(int[] expected, int[] l1, int[] l2, int[] l3)
    {
        ListNode[] nodes = [ListNode.FromArray(l1), ListNode.FromArray(l2), ListNode.FromArray(l3)];

        var res = MergeKLists(nodes);

        var arr = res.ToArray();
        Assert.Equal(expected, arr);

    }


    [Fact]
    public void TestIt2()
    {
        ListNode[] nodes = [ListNode.FromArray([])];

        var res = MergeKLists(nodes);

        var arr = res.ToArray();
        Assert.Equal(Array.Empty<int>(), arr);
    }


    [Fact]
    public void TestIt3()
    {
        ListNode[] nodes = [];

        var res = MergeKLists(nodes);

        var arr = res.ToArray();
        Assert.Equal(Array.Empty<int>(), arr);
    }

    public ListNode MergeKLists(ListNode[] lists)
    {

        ListNode Merge(ListNode a, ListNode b)
        {
            if (a == null && b == null)
                return null;
            if (a == null)
                return b;
            if (b == null)
                return a;

            var res = new ListNode();
            var res2 = res;

            while (a != null && b != null)
            {
                if (a.val > b.val)
                {
                    res.val = b.val;
                    b = b.next;
                }
                else
                {
                    res.val = a.val;
                    a = a.next;
                }

                if (a != null || b != null)
                {
                    res.next = new();
                    res = res.next;
                }

            }


            while (a != null)
            {
                res.val = a.val;
                a = a.next;
                if (a != null)
                {
                    res.next = new();
                    res = res.next;
                }
            }

            while (b != null)
            {
                res.val = b.val;
                b = b.next;
                if (b != null)
                {
                    res.next = new();
                    res = res.next;
                }
            }

            return res2;
        }

        if (lists.Length == 0)
            return null;

        var m = lists[0];
        for (int i = 1; i < lists.Length; ++i)
        {
            m = Merge(m, lists[i]);
        }

        return m;
    }
}