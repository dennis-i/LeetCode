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



   


}