using System.Diagnostics;

namespace LCode;



public class WhenTrainingForFbNodesInASubtree
{
    [Fact]
    public void TestIt()
    {

        //Testcase 1

        string s = "aba";
        Node root = new Node(1);
        root.children.Add(new Node(2));
        root.children.Add(new Node(3));
        List<Query> queries = [new(1, 'a')];

        int[] output = CountOfNodes(root, queries, s);
        int[] expected = [2];
        Assert.Equal(expected, output);


        // Testcase 2

        s = "abaacab";
        root = new Node(1);
        root.children.Add(new Node(2));
        root.children.Add(new Node(3));
        root.children.Add(new Node(7));
        root.children[0].children.Add(new Node(4));
        root.children[0].children.Add(new Node(5));
        root.children[1].children.Add(new Node(6));
        queries =
        [
           new(1, 'a'),
            new(2, 'b'),
            new(3, 'a')
        ];



        output = CountOfNodes(root, queries, s);
        expected = [4, 1, 2];
        Assert.Equal(expected, output);

    }

    int[] CountOfNodes(Node root, List<Query> queries, string s)
    {

        Node findNode(Node node, Query query)
        {
            var queue = new Queue<Node>();
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                var curr = queue.Dequeue();
                if (curr is not null)
                {

                    if (curr.val == query.u)
                        return curr;
                    foreach (var child in curr.children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }

            return null;
        }


        int countFrom(Node node, string str, char c)
        {
            if (node == null)
                return 0;

            int sIdx = node.val - 1;
            int res = str[sIdx] != c ? 0 : 1;

            foreach (var child in node.children)
            {
                res += countFrom(child, str, c);
            }

            return res;
        }

        var l = new List<int>(queries.Count);
        foreach (var query in queries)
        {
            var node = findNode(root, query);
            int n = countFrom(node, s, query.c);
            l.Add(n);
        }
        return l.ToArray();

    }


    void Dump(Node root, string s, int space)
    {
        if (root == null)
            return;
        var sp = Enumerable.Range(0, space).Select(_ => ' ').ToArray();
        Debug.Write($"{new string(sp)}{root.val}({s[root.val - 1]})\n");
        foreach (var child in root.children)
        {
            Dump(child, s, space + 10);
        }
    }


    class Node
    {
        public int val;
        public List<Node> children;

        public Node()
        {
            val = 0;
            children = new List<Node>();
        }

        public Node(int _val)
        {
            val = _val;
            children = new List<Node>();
        }

        public Node(int _val, List<Node> _children)
        {
            val = _val;
            children = _children;
        }
    }

    class Query
    {
        public int u;
        public char c;
        public Query(int u, char c)
        {
            this.u = u;
            this.c = c;
        }
    }

}