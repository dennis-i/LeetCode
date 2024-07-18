namespace LCode;

public class WhenTesting_RemoveNthNodeFromEndOfList
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 5 }, new[] { 1, 2, 3, 4, 5 }, 2)]
    public void TestIt(int[] expected, int[] nums, int n)
    {
        ListNode head = ListNode.FromArray(nums);
        var res = RemoveNthFromEnd(head, n);
        Assert.Equal(expected, res.ToArray());
    }

    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {

        var slow = head;
        var fast = head;

        int fastCnt = 0;
        int slowCnt = 0;
        while (slow != null)
        {

            if (fast != null)
            {

                ++fastCnt;
                fast = fast.next;
            }

            if (fast != null)
            {

                ++fastCnt;
                fast = fast.next;
            }

            if (fast == null)
            {
                if (slowCnt == fastCnt)
                {
                    slow.next = slow.next.next;
                    break;
                }
            }
            slow = slow.next;
            ++slowCnt;
        }

        return head;
    }
}