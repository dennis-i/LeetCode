namespace LCode;

public class WhenTesting_MergeTwoSortedLists
{


    [Theory]
    [InlineData(new[] { 1, 1, 2, 3, 4, 4 }, new[] { 1, 2, 4 }, new[] { 1, 3, 4 })]
    [InlineData(new[] { 0 }, new int[0], new[] { 0 })]
    public void TestIt(int[] expected, int[] l1, int[] l2)
    {
        var merged = MergeTwoLists(ListNode.FromArray(l1), ListNode.FromArray(l2));
        Assert.Equal(expected, merged.ToArray());
    }


    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        if (list1 == null && list2 == null)
            return null;
        var merged = new ListNode();
        Merge(list1, list2, merged);
        return merged;
    }

    private void Merge(ListNode l1, ListNode l2, ListNode merged)
    {
        if (l1 != null && l2 != null)
        {
            merged.next = new();
            if (l1.val < l2.val)
            {
                merged.val = l1.val;
                Merge(l1.next, l2, merged.next);
            }
            else
            {
                merged.val = l2.val;
                Merge(l1, l2.next, merged.next);
            }
        }
        else if (l1 == null && l2 != null)
        {
            merged.next = l2.next;
            merged.val = l2.val;
        }
        else if (l1 != null)
        {
            merged.next = l1.next;
            merged.val = l1.val;
        }
    }
}