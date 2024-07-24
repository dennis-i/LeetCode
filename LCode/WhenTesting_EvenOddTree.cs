namespace LCode;

public class WhenTesting_EvenOddTree
{
    [Theory]
    [InlineData(true, new object[] { 1, 10, 4, 3, null, 7, 9, 12, 8, 6, null, null, 2 })]
    [InlineData(false, new object[] { 5, 4, 2, 3, 3, 7 })]
    [InlineData(false, new object[] { 5, 9, 1, 3, 5, 7 })]
    public void TestIt(bool expected, object[] root)
    {
        var node = TreeNode.CreateTreeFromArray(root);



        Assert.Equal(expected, IsEvenOddTree(node));
    }
    public bool IsEvenOddTree(TreeNode root)
    {


        bool isOddIncreasing(ReadOnlySpan<TreeNode> nodes)
        {
            if (nodes.IsEmpty) return true;
            for (int i = 1; i < nodes.Length; ++i)
            {
                var n1 = nodes[i - 1].val;
                var n2 = nodes[i].val;
                if (n1 >= n2)
                    return false;
                if (int.IsEvenInteger(n2))
                    return false;
            }
            return int.IsOddInteger(nodes[0].val);
        }

        bool isEvenDecreasing(ReadOnlySpan<TreeNode> nodes)
        {
            if (nodes.IsEmpty) return true;
            for (int i = 1; i < nodes.Length; ++i)
            {
                var n1 = nodes[i - 1].val;
                var n2 = nodes[i].val;
                if (n1 <= n2)
                    return false;
                if (int.IsOddInteger(n2))
                    return false;
            }
            return int.IsEvenInteger(nodes[0].val);
        }

        Queue<TreeNode> queue = new();
        bool odd = true;
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var arr = new TreeNode[queue.Count];
            int idx = 0;
            while (queue.TryDequeue(out var node))
            {
                if (node != null)
                    arr[idx++] = node;
            }

            bool correct = odd ? isOddIncreasing(arr) : isEvenDecreasing(arr);
            if (!correct)
                return false;
            odd = !odd;
            foreach (var item in arr)
            {
                if (item.left != null)
                    queue.Enqueue(item.left);
                if (item.right != null)
                    queue.Enqueue(item.right);
            }
        }

        return true;
    }
}