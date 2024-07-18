namespace LCode;


public class WhenTesting_RemoveNthNodeFromEndOfList
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 5 }, new[] { 1, 2, 3, 4, 5 }, 2)]
    [InlineData(new int[0], new[] { 1 }, 1)]
    [InlineData(new[] { 1 }, new[] { 1, 2 }, 1)]
    [InlineData(new[] { 2, 3 }, new[] { 1, 2, 3 }, 3)]
    public void TestIt(int[] expected, int[] nums, int n)
    {
        ListNode head = ListNode.FromArray(nums);
        var res = RemoveNthFromEnd(head, n);
        var arr = res.ToArray();
        Assert.Equal(expected, arr);
    }

    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {


        void helper(ref ListNode slow, ListNode fast, int fastCnt, int slowCnt)
        {
            if (slow != null)
            {

                int stepCnt = n;

                while (fast != null && stepCnt > 0)
                {
                    --stepCnt;
                    ++fastCnt;
                    fast = fast.next;
                }

                if (fast == null)
                {
                    if (slowCnt == fastCnt - n)
                    {
                        slow = slow.next;
                        return;
                    }
                }
                ++slowCnt;
                helper(ref slow.next, fast, fastCnt, slowCnt);
            }
        }

        int fastCnt = 0;
        int slowCnt = 0;

        helper(ref head, head, fastCnt, slowCnt);

        return head;
    }
}