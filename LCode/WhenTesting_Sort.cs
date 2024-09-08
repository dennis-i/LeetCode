
using Raylib_CsLo;
using System.Diagnostics;
using System.Text;

namespace LCode;




public class WhenTestingHeap
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 100 }, new[] { 100, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 })]
    public void TestIt(int[] expected, int[] nums)
    {
        var mh = new MinHeap();

        foreach (var n in nums)
        {
            mh.Add(n);
        }
        string dump = mh.Dump();
        RunRay(mh.Root);
    }

    private void RunRay(TreeNode root)
    {
        Raylib.InitWindow(1280, 720, "Hello, Raylib-CsLo");
        Raylib.SetTargetFPS(60);


        const int NodeRadius = 30;
        const int Node2NodeDist = NodeRadius * 2;
        const int FontSize = NodeRadius >> 1;

        // Main game loop
        while (!Raylib.WindowShouldClose()) // Detect window close button or ESC key
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.SKYBLUE);


            void drawNode(int x, int y, TreeNode node)
            {
                if (node == null)
                    return;
                Raylib.DrawCircle(x, y, NodeRadius, Raylib.YELLOW);
                Raylib.DrawText($"{node.val}", x - (FontSize >> 1), y - (FontSize >> 1), FontSize, Raylib.BLACK);
            }

            int x = Raylib.GetRenderWidth() >> 1;
            int y = (int)Math.Round(NodeRadius * 1.5);

            var q = new Queue<(int, TreeNode)>();
            q.Enqueue((0, root));
            while (q.Count > 0)
            {
                var cur = q.Dequeue();

                int idx = cur.Item1;
                TreeNode tn = cur.Item2;
                if (tn != null)
                {
                    drawNode(x - Node2NodeDist * idx, y + Node2NodeDist * idx, tn);
                    drawNode(x + Node2NodeDist * idx, y + Node2NodeDist * idx, tn);
                    q.Enqueue((idx + 1, tn.left));
                    q.Enqueue((idx + 1, tn.right));

                }

            }








            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }


    class MinHeap
    {
        private TreeNode _root = null;
        public TreeNode Root => _root;
        public string Dump()
        {
            var sb = new StringBuilder();

            var q = new Queue<TreeNode>();



            if (_root != null)
            {
                sb.Append($"{_root.val}\n");
                q.Enqueue(_root);
            }

            while (q.Count > 0)
            {
                var curr = q.Dequeue();
                if (curr.left != null)
                {
                    sb.Append($"{curr.left.val} ");
                    q.Enqueue(curr.left);
                }
                if (curr.right != null)
                {
                    sb.Append($"{curr.right.val} ");
                    q.Enqueue(curr.right);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
        public void Add(int val)
        {
            var node = new TreeNode(val);
            Add(ref _root, node);

        }

        private void Add(ref TreeNode root, TreeNode newNode)
        {

            if (root == null)
                root = newNode;
            else
            {
                if (root.left != null && root.right == null)
                    Add(ref root.right, newNode);
                else
                    Add(ref root.left, newNode);
            }
        }
    }
}

public class WhenFindingPrimes
{
    [Theory]
    [InlineData(10)]
    [InlineData(100000)]
    [InlineData(int.MaxValue)]
    public void TestIt(int n)
    {
        int numPrimes = NumPrimes(n);
        Debug.WriteLine($"there are {numPrimes} primes in range of 0 to {n}");
    }

    private int NumPrimes(int n)
    {
        for (int i = 5; i < 100; i += 5)
            Debug.WriteLine($"0x{i:X2}");



        static bool IsPrime(int candidate)
        {


            if (int.IsOddInteger(candidate) && candidate % 5 != 0)
            {
                int limit = (int)Math.Sqrt(candidate);


                for (int divisor = 3; divisor <= limit; divisor += 2)
                {
                    if ((candidate % divisor) == 0)
                        return false;
                }
                return true;
            }
            return candidate == 2 || candidate == 5;
        }

        List<int> l = [2, 3];
        for (int i = 4; i <= n; ++i)
            if (IsPrime(i))
                l.Add(i);

        return l.Count;
    }
}

public class WhenTesting_Sort
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 100 }, new[] { 100, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 })]
    public void TestIt(int[] expected, int[] nums)
    {
        Sort(nums, 0, nums.Length - 1);
        Assert.Equal(expected, nums);
    }


    [Theory]
    //[InlineData(10)]
    //[InlineData(20)]
    //[InlineData(30)]
    //[InlineData(40)]
    //[InlineData(50)]
    //[InlineData(70)]
    [InlineData(120)]
    public void TestIt2(int arrLen)
    {
        int[] nums1 = new int[arrLen];
        int[] nums2 = new int[arrLen];
        int[] expected = new int[arrLen];
        for (int i = 0; i < arrLen; ++i)
        {
            nums1[i] = arrLen - i;
            expected[i] = i + 1;
        }


        var sw = new Stopwatch();
        sw.Start();
        Array.Sort(nums2);

        sw.Stop();
        Debug.WriteLine($"sort 2 took:{sw.Elapsed.TotalNanoseconds} ns");
        sw.Restart();
        Sort(nums1);
        sw.Stop();
        Debug.WriteLine($"sort 1 took:{sw.Elapsed.TotalNanoseconds} ns");
        Assert.Equal(expected, nums1);
    }

    private void Sort(int[] nums)
    {
        _numIt = 0;
        Sort(nums, 73);
        //Sort(nums, 23);
        Sort(nums, 17);
        //Sort(nums, 11);
        Sort(nums, 7);
        //Sort(nums, 5);
        Sort(nums, 3);
        Sort(nums, 2);
    }
    private static int _numIt = 0;
    private void Sort(int[] nums, int step)
    {

        int cnt = 0;
        while (cnt < nums.Length - step)
        {
            var l = nums.AsSpan(cnt, step);
            var r = nums.AsSpan(nums.Length - step - cnt, step);
            ++cnt;

            void order(Span<int> span)
            {
                if (span[0] > span[^1])
                    (span[0], span[^1]) = (span[^1], span[0]);
                _numIt++;
            }
            order(l);
            order(r);
        }
    }


    public static void Sort(int[] array, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(array, low, high);
            Sort(array, low, pivotIndex - 1);
            Sort(array, pivotIndex + 1, high);
        }
    }


    private static int Partition(int[] array, int low, int high)
    {

        void Swap(Span<int> span, int idx1, int idx2) => (span[idx1], span[idx2]) = (span[idx2], span[idx1]);


        int pivot = array[high];
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }
        Swap(array, i + 1, high);
        return i + 1;
    }


}


