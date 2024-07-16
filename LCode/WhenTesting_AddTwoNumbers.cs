namespace LCode;

public class WhenTesting_AddTwoNumbers
{


    [Theory]
    [InlineData(new[] { 7, 0, 8 }, new[] { 2, 4, 3 }, new[] { 5, 6, 4 })]
    [InlineData(new[] { 0 }, new[] { 0 }, new[] { 0 })]
    [InlineData(new[] { 8, 9, 9, 9, 0, 0, 0, 1 }, new[] { 9, 9, 9, 9, 9, 9, 9 }, new[] { 9, 9, 9, 9 })]
    [InlineData(new[] { 1,8 }, new[] { 1, 8 }, new[] { 0 })]
    public void TestIt(int[] expected, int[] l1, int[] l2)
    {
        var res = AddTwoNumbers(ListNode.FromArray(l1), ListNode.FromArray(l2));
        Assert.Equal(expected, res.ToArray());
    }

    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        ListNode res = new ListNode();
        Add(l1, l2, 0, res);
        return res;
    }



    private void Add(ListNode l1, ListNode l2, int carry, ListNode result)
    {
        if (l1 != null && l2 != null)
        {
            result.val = (l1.val + l2.val + carry) % 10;
            var c = (l1.val + l2.val + carry) / 10;
            if (l1.next != null || l2.next != null || c != 0)
                result.next = new();
            Add(l1.next, l2.next, c, result.next);
        }
        else if (l1 != null)
        {
            result.val = (l1.val + carry) % 10;
            var c = (l1.val + carry) / 10;
            if (l1.next != null || c != 0)
                result.next = new();
            Add(l1.next, l2, c, result.next);
        }
        else if (l2 != null)
        {
            result.val = (l2.val + carry) % 10;
            var c = (l2.val + carry) / 10;
            if (l2.next != null || c != 0)
                result.next = new();
            Add(l1, l2.next, c, result.next);
        }
        else if (carry != 0 && result != null)
        {
            result.val = carry;
        }


    }
}