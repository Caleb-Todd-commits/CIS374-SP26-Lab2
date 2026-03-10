using BenchmarkDotNet.Attributes;
using DSA.DataStructures.Trees;

namespace Lab1
{
    [MemoryDiagnoser]
    [ShortRunJob]
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter]
    public class HeightKeyValueMapBenchmarks
    {
        public enum KeyValueMapType
        {
            BST,
            AVL,
            RedBlack
        }

        [Params(100, 1_000, 10_000, 100_000, 1_000_000)]
        public int N;

        [Params(true, false)]
        public bool isInOrder;

        [Params(KeyValueMapType.BST, KeyValueMapType.AVL, KeyValueMapType.RedBlack)]
        public KeyValueMapType keyValueMapType;

        [Benchmark]
        public double Height()
        {
            int trials = GetTrialCount();
            long totalHeight = 0;

            for (int i = 0; i < trials; i++)
            {
                var keyValuePairs = BuildInput();

                switch (keyValueMapType)
                {
                    case KeyValueMapType.BST:
                    {
                        var bst = new BinarySearchTreeMap<int, int>();
                        foreach (var kvp in keyValuePairs)
                        {
                            bst.Add(kvp.Key, kvp.Value);
                        }

                        totalHeight += bst.Height;
                        break;
                    }
                    case KeyValueMapType.AVL:
                    {
                        var avlTree = new AVLTreeMap<int, int>();
                        foreach (var kvp in keyValuePairs)
                        {
                            avlTree.Add(kvp.Key, kvp.Value);
                        }

                        totalHeight += avlTree.Height;
                        break;
                    }
                    case KeyValueMapType.RedBlack:
                    {
                        var redBlackTree = new RedBlackTreeMap<int, int>();
                        foreach (var kvp in keyValuePairs)
                        {
                            redBlackTree.Add(kvp.Key, kvp.Value);
                        }

                        totalHeight += redBlackTree.Height;
                        break;
                    }
                }
            }

            return (double)totalHeight / trials;
        }

        private int GetTrialCount()
        {
            if (N >= 1_000_000)
            {
                return 1;
            }

            if (N >= 100_000)
            {
                return 3;
            }

            return 5;
        }

        private List<KeyValuePair<int, int>> BuildInput()
        {
            var keyValuePairs = new List<KeyValuePair<int, int>>(N);
            for (int i = 0; i < N; i++)
            {
                keyValuePairs.Add(new KeyValuePair<int, int>(i, i * 10));
            }

            if (!isInOrder)
            {
                keyValuePairs.Shuffle();
            }

            return keyValuePairs;
        }
    }
}
