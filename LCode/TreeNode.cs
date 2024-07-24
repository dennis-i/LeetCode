using System.Text;

namespace LCode;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }

    public string Dump()
    {

        void recDump(TreeNode node, StringBuilder stringBuilder, int level)
        {
            if (node != null)
            {
                char[] spaces = new char[level];
                Array.Fill(spaces, ' ');
                recDump(node.left, stringBuilder, level + 5);
                stringBuilder.Append(spaces);
                stringBuilder.AppendFormat("{0}\n", node.val);
                recDump(node.right, stringBuilder, level + 5);
            }
        }

        var sb = new StringBuilder();

        recDump(this, sb, 1);
        return sb.ToString();
    }

    public static TreeNode CreateTreeFromArray(object[] values)
    {
        if (values.Length == 0) return null;
        TreeNode root = null;
        if (values[0] is int val)
        {


            root = new TreeNode(val);
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            int i = 1;
            while (i < values.Length)
            {
                TreeNode current = queue.Dequeue();
                if (i < values.Length)
                {
                    if (values[i++] is int vl)
                    {
                        current.left = new TreeNode(vl);
                        queue.Enqueue(current.left);
                    }

                }
                if (i < values.Length)
                {
                    if (values[i++] is int vr)
                    {
                        current.right = new TreeNode(vr);
                        queue.Enqueue(current.right);
                    }

                }
            }
        }
        return root;
    }

   
}