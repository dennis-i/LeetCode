namespace LCode;

public class WhenTesting_SameTree
{

    [Theory]
    [InlineData(true, new object[] { 1, 2, 3 }, new object[] { 1, 2, 3 })]
    [InlineData(false, new object[] { 1, 2 }, new object[] { 1, null, 2 })]
    [InlineData(false, new object[] { 1, 2, 1 }, new object[] { 1, 1, 2 })]
    public void TestIt(bool expected, object[] p, object[] q)
    {
        var treeP = TreeNode.CreateTreeFromArray(p);
        var treeQ = TreeNode.CreateTreeFromArray(q);
        Assert.Equal(expected, IsSameTree(treeP, treeQ));
    }

    public bool IsSameTree(TreeNode p, TreeNode q)
    {

        Queue<TreeNode> pfifo = new();
        Queue<TreeNode> qfifo = new();
        pfifo.Enqueue(p);
        qfifo.Enqueue(q);
        while (pfifo.Count > 0)
        {
            var n1 = pfifo.Dequeue();
            if (!qfifo.TryDequeue(out var n2))
                return false;


            if (n1 == null && n2 == null)
                continue;
            if (n2 != null && n1 != null)
            {
                if (n2.val != n1.val)
                    return false;
                pfifo.Enqueue(n1.left);
                pfifo.Enqueue(n1.right);
                qfifo.Enqueue(n2.left);
                qfifo.Enqueue(n2.right);
            }
            else
            {
                return false;
            }

        }

        return qfifo.Count == 0;
    }
}