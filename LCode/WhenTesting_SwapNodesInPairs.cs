namespace LCode;

public class WhenTesting_SwapNodesInPairs
{
    [Theory]

    [InlineData(new[] { 2, 1, 4, 3 }, new[] { 1, 2, 3, 4 })]
    [InlineData(new[] { 1 }, new[] { 1 })]
    [InlineData(new int[0], new int[0])]

    public void TestIt(int[] expected, int[] input)
    {

        ListNode root = ListNode.FromArray(input);

        var swapped = SwapPairs(root);

        var testArray = swapped == null ? Array.Empty<int>() : swapped.ToArray();

        Assert.Equal(expected, testArray);
    }



    public ListNode SwapPairs(ListNode head)
    {
        if (head is null)
            return null;
        Stack<ListNode> stack = new();
        stack.Push(head);
        stack.Push(head.next);
        while (stack.Count > 0)
        {

            var second = stack.Pop();
            var first = stack.Pop();

            if (first != null && second != null)
            {
                (first.val, second.val) = (second.val, first.val);

                if (second.next != null)
                {
                    stack.Push(second.next);
                    stack.Push(second.next.next);
                }
            }
        }
        return head;
    }



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


}

