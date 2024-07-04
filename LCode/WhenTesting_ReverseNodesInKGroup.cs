namespace LCode;

//TODO
public class WhenTesting_ReverseNodesInKGroup
{
    [Theory]

    [InlineData(new[] { 2, 1 }, new[] { 1, 2 }, 2)]
    [InlineData(new[] { 2, 1, 4, 3, 5 }, new[] { 1, 2, 3, 4, 5 }, 2)]
    public void TestIt(int[] expected, int[] input, int k)
    {
        ListNode root = ListNode.FromArray(input);

        var swapped = ReverseKGroup(root, k);

        var testArray = swapped == null ? Array.Empty<int>() : swapped.ToArray();

        Assert.Equal(expected, testArray);
    }


    public ListNode ReverseKGroup(ListNode head, int k)
    {




        int cnt = 0;
        Stack<ListNode> stack = new();
        Queue<ListNode> queue = new();





        stack.Push(head);
        queue.Enqueue(head);
        while (queue.Count < k)
        {
            var n = stack.Peek();
            if (n == null)
                break;


            stack.Push(n.next);
            queue.Enqueue(n.next);
        }

        head = stack.Peek();
        while (queue.Count > 0)
        {
            var first = queue.Dequeue();
            var last = stack.Pop();
            (first.next, last.next) = (last.next, first.next);

        }


        return head;
    }
}