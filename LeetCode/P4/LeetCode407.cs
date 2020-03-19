using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCode.P4
{
    public class LeetCode407
    {
        private readonly ILogger _logger;

        public LeetCode407(ILogger logger) => _logger = logger;

        public int TrapRainWater(int[][] heightMap)
        {
            if (heightMap.Length < 3) return 0;
            int xLength = heightMap.Length, yLength = heightMap.Max(m => m.Length);
            if (yLength < 3) return 0;

            var height = new PositionInfo[xLength, yLength];
            for (var x = 0; x < heightMap.Length; x++)
            {
                var array = heightMap[x];
                for (var y = 0; y < yLength; y++)
                {
                    height[x, y] = new PositionInfo(y >= array.Length ? 0 : array[y], x, y);
                }
            }

            var numberLength = (int)Math.Log10(heightMap.SelectMany(_ => _).Max()) + 1;
            //Dump(height, numberLength, xLength, yLength);

            ComputeWall(height, xLength, yLength);

            //Dump(height, numberLength, xLength, yLength);

            return (from PositionInfo p in height where p.Height > p.Value select p.Height - p.Value).Sum();
        }

        void ComputeWall(PositionInfo[,] heightMap, int xLength, int yLength)
        {
            for (var x = 1; x < xLength - 1; x++)
                for (var y = 1; y < yLength - 1; y++)
                {
                    var position = heightMap[x, y];
                    if (position.Height >= 0) continue;

                    position = FindLowLying(heightMap, xLength, yLength, position);

                    if (position == null || position.Height >= 0) continue;

                    ComputePoint(heightMap, xLength, yLength, null,
                        new SortedSet<PositionInfo>(),
                        new SortedSet<PositionInfo>(new PositionInfoValueComparer()),
                        new HashSet<PositionInfo> { position });
                }
        }

        /// <summary>通过当前点查找最低点</summary>
        PositionInfo FindLowLying(PositionInfo[,] heightMap, int xLength, int yLength, PositionInfo position)
        {
            var computing = new HashSet<PositionInfo>();
            while (true)
            {
                computing.Add(position);

                var edges = new SortedSet<PositionInfo>(new PositionInfoValueComparer());
                foreach (var b in Bounds)
                {
                    edges.Add(heightMap[position.X + b[0], position.Y + b[1]]);
                }

                var min = edges.First(p => !computing.Contains(p));
                if (min.Value > position.Value) return position;
                if (min.Height >= 0)
                {
                    position.Height = min.Height;
                    return null;
                }
                if (min.X < 1 || min.Y < 1 || min.X > xLength - 2 || min.Y > yLength - 2)
                {
                    foreach (var p in computing)
                    {
                        p.Height = p.Value; //如果找不到，则整个线上都是最小
                    }

                    return null;
                }
                position = min;
            }
        }

        private static readonly int[][] Bounds = { new[] { 0, -1 }, new[] { 0, 1 }, new[] { -1, 0 }, new[] { 1, 0 } };

        /// <summary>计算低点的高度</summary>
        int ComputePoint(PositionInfo[,] heightMap, int xLength, int yLength, int? minHeight,
            ISet<PositionInfo> computing,
            ISet<PositionInfo> edges,
            ISet<PositionInfo> lowLying)
        {
            foreach (var ll in lowLying)
            {
                foreach (var b in Bounds)
                    edges.Add(heightMap[ll.X + b[0], ll.Y + b[1]]);

                computing.Add(ll);
            }

            var height = ComputeWall(heightMap, xLength, yLength, minHeight, computing, edges, lowLying);
            foreach (var position in lowLying)
            {
                if (position.Height < height) position.Height = height;
            }
            return height;
        }

        /// <summary>循环计算池的高度</summary>
        int ComputeWall(PositionInfo[,] heightMap, int xLength, int yLength, int? minHeight,
            ISet<PositionInfo> computing,
            ISet<PositionInfo> edges,
            ISet<PositionInfo> lowLying)
        {
            while (true)
            {
                var min = edges.FirstOrDefault(p => !computing.Contains(p));
                if (min == null) return int.MaxValue;

                if (minHeight.HasValue && min.Value >= minHeight) return minHeight.Value;

                var height = ComputeHeight(heightMap, xLength, yLength, min, computing, edges, lowLying);
                if (height >= 0)
                {
                    if (height > minHeight.GetValueOrDefault(height))
                    {
                        foreach (var ll in lowLying)
                        {
                            if (ll.Height >= 0 && ll.Height != height) throw new Exception($"池高错误：{ll}:{ll.Height}新{height}");
                            if (ll.Value < height) ll.Height = height;
                            else throw new Exception($"池高错误：{ll}:{ll.Value}大于等于{height}");
                        }

                        foreach (var ll in edges)
                        {
                            if (ll.Height >= 0 && ll.Height != height) throw new Exception($"池高错误：{ll}:{ll.Height}新{height}");
                            if (ll.Value >= height) ll.Height = height;
                            else throw new Exception($"边高错误：{ll}:{ll.Value}小于{height}");
                        }
                    }

                    return height;
                }
            }
        }

        /// <summary>计算点的高度</summary>
        int ComputeHeight(PositionInfo[,] heightMap, int xLength, int yLength, PositionInfo position,
            ISet<PositionInfo> computing,
            ISet<PositionInfo> edges,
            ISet<PositionInfo> lowLying)
        {
            if (position.X < 1 || position.Y < 1 || position.X > xLength - 2 || position.Y > yLength - 2) return position.Value;

            computing.Add(position);

            if (position.Height >= position.Value) return position.Height;

            var e1 = new SortedSet<PositionInfo>();
            var l1 = new List<PositionInfo>();

            foreach (var b in Bounds)
            {
                var p = heightMap[position.X + b[0], position.Y + b[1]];
                if (computing.Contains(p)) continue;

                if (position.Value <= p.Value)
                {
                    edges.Add(p);
                }
                else
                {
                    if (p.X < 1 || p.Y < 1 || p.X > xLength - 2 || p.Y > yLength - 2)
                    {
                        //如果p是边点时且小于当前点，则当前点为墙
                        if (position.Value > p.Value) return position.Value;
                    }
                    else
                    {
                        var ll = FindLowLying(heightMap, xLength, yLength, p);
                        //找不到最低点则当前点为墙
                        if (ll == null) return position.Value;

                        var e2 = new SortedSet<PositionInfo>(new PositionInfoValueComparer());
                        var l2 = new SortedSet<PositionInfo> { ll };

                        //当前点是另一低点的墙时，则当前低点计算完成
                        if (position.Value > ComputePoint(heightMap, xLength, yLength, position.Value, computing, e2, l2)) return position.Value;

                        foreach (var a in e2) if (!l1.Contains(a)) e1.Add(a);
                        foreach (var a in l2) if (!e1.Contains(a)) l1.Add(a);
                    }
                }
            }

            foreach (var a in e1) edges.Add(a);
            foreach (var a in l1) lowLying.Add(a);

            lowLying.Add(position);
            edges.Remove(position);

            return -1;
        }

        class PositionInfo : IComparable<PositionInfo>, IComparable
        {
            public PositionInfo(int value, int x, int y)
            {
                Value = value;
                X = x;
                Y = y;
            }

            public int Value { get; }
            public int X { get; }
            public int Y { get; }
            public int Height { get; set; } = -1;

            public override string ToString() => $"{X},{Y},{Value}";

            public int CompareTo(PositionInfo other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (other is null) return 1;
                var xComparison = X.CompareTo(other.X);
                if (xComparison != 0) return xComparison;
                return Y.CompareTo(other.Y);
            }

            public int CompareTo(object obj)
            {
                if (obj is null) return 1;
                if (ReferenceEquals(this, obj)) return 0;
                return obj is PositionInfo other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(PositionInfo)}");
            }

            public override bool Equals(object obj) => CompareTo(obj) == 0;

            public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
        }

        class PositionInfoValueComparer : IComparer<PositionInfo>
        {
            public int Compare(PositionInfo x, PositionInfo y)
            {
                if (x is null) return y is null ? 0 : -1;

                if (y is null) return 1;

                if (ReferenceEquals(x, y)) return 0;

                var result = x.Value - y.Value;
                if (result != 0) return result;

                result = x.X - y.X;
                if (result != 0) return result;

                return x.Y - y.Y;
            }
        }

        private void Dump(PositionInfo[,] map, int numberLength, int xLength, int yLength)
        {
            var sb = new StringBuilder();

            for (var x = 0; x < xLength; x++)
            {
                if (x > 0) sb.Append(',').AppendLine();

                sb.Append('{');
                for (var y = 0; y < yLength; y++)
                {
                    var p = map[x, y];

                    if (y > 0) sb.Append(", ");

                    sb.AppendFormat($"{{0,{numberLength}}}", p.Value).Append(':').AppendFormat($"{{0,{numberLength}}}", p.Value >= p.Height ? 0 : p.Height);
                }

                sb.Append('}');
            }

            _logger.LogInformation(sb.ToString());
        }
    }
}
