namespace LCode;

public class ListNode
{
    public int val;
    public ListNode next;

    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }

    public static ListNode FromArray(int[] input)
    {

        var s = input.AsSpan();
        if (s.IsEmpty)
            return null;

        ListNode root = new ListNode(s[0]);
        if (s.Length == 1)
            return root;
        Generate(root, s.Slice(1));
        return root;
    }

    private static void Generate(ListNode root, Span<int> input)
    {
        if (!input.IsEmpty)
        {
            root.next = new ListNode(input[0]);
            Generate(root.next, input.Slice(1));
        }
    }

    private static void Generate(ListNode root, List<int> list)
    {
        list.Add(root.val);
        if (root.next != null)
        {
            Generate(root.next, list);
        }
    }




    public IReadOnlyList<int> ToArray()
    {
        var l = new List<int>();
        Generate(this, l);
        return l;
    }
}