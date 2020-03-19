//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace LeetCode.P4
//{
//    public class LeetCode407
//    {
//        private readonly ILogger _logger;

//        public LeetCode407(ILogger logger) => _logger = logger;

//        public int TrapRainWater(int[][] heightMap)
//        {
//            if (heightMap.Length < 3) return 0;
//            int xLength = heightMap.Length, yLength = heightMap.Max(m => m.Length);
//            if (yLength < 3) return 0;

//            var height = new PositionInfo[xLength, yLength];
//            for (var x = 0; x < heightMap.Length; x++)
//            {
//                var array = heightMap[x];
//                for (var y = 0; y < yLength; y++)
//                {
//                    height[x, y] = new PositionInfo(y >= array.Length ? 0 : array[y], x, y);
//                }
//            }

//            var numberLength = (int)Math.Log10(heightMap.SelectMany(_ => _).Max()) + 1;
//            //Dump(height, numberLength, xLength, yLength);
//            Dump2(height, numberLength, xLength, yLength);

//            ComputeWall(height, xLength, yLength, FindLowLying(height, xLength, yLength));

//            Dump(height, numberLength, xLength, yLength);

//            return (from PositionInfo p in height where p.Height > 0 select p.Height - p.Value).Sum();
//        }

//        IEnumerable<IList<PositionInfo>> FindLowLying(PositionInfo[,] heightMap, int xLength, int yLength)
//        {
//            var lowLying = new SortedSet<PositionInfo>();
//            for (var x = 1; x < xLength - 1; x++)
//                for (var y = 1; y < yLength - 1; y++)
//                {
//                    var position = heightMap[x, y];
//                    if (lowLying.Contains(position)) continue;

//                    var l = heightMap[x, y - 1];
//                    var r = heightMap[x, y + 1];
//                    var n = heightMap[x - 1, y];
//                    var s = heightMap[x + 1, y];

//                    if (position.Value > l.Value || position.Value > r.Value || position.Value > n.Value || position.Value > s.Value) continue;

//                    if (position.Value < l.Value && position.Value < r.Value && position.Value < n.Value && position.Value < s.Value)
//                        yield return new List<PositionInfo> { position };
//                    else
//                    {
//                        var cache = new SortedSet<PositionInfo> { position };
//                        var list = new List<PositionInfo> { position };

//                        if ((position.Value < l.Value || FindOther(l, cache, list)) &&
//                            (position.Value < r.Value || FindOther(r, cache, list)) &&
//                            (position.Value < n.Value || FindOther(n, cache, list)) &&
//                            (position.Value < s.Value || FindOther(s, cache, list)))
//                            yield return list;
//                    }
//                }

//            bool FindOther(PositionInfo position, ISet<PositionInfo> cache, ICollection<PositionInfo> list)
//            {
//                if (position.X < 1 || position.Y < 1 || position.X > xLength - 2 || position.Y > yLength - 2) return false;

//                if (cache.Contains(position)) return true;

//                cache.Add(position);

//                var l = heightMap[position.X, position.Y - 1];
//                var r = heightMap[position.X, position.Y + 1];
//                var n = heightMap[position.X - 1, position.Y];
//                var s = heightMap[position.X + 1, position.Y];

//                if (position.Value > l.Value || position.Value > r.Value || position.Value > n.Value || position.Value > s.Value) return false;

//                if (position.Value == l.Value && !FindOther(l, cache, list) ||
//                    position.Value == r.Value && !FindOther(r, cache, list) ||
//                    position.Value == n.Value && !FindOther(n, cache, list) ||
//                    position.Value == s.Value && !FindOther(s, cache, list)) return false;

//                lowLying.Add(position);
//                list.Add(position);

//                return true;
//            }
//        }

//        void ComputeWall(PositionInfo[,] heightMap, int xLength, int yLength, IEnumerable<IList<PositionInfo>> lowLying)
//        {
//            foreach (var lls in lowLying)
//            {
//                var height = ComputeWall(heightMap, xLength, yLength, null,
//                    new SortedSet<PositionInfo>(), new SortedSet<PositionInfo>(), lls);
//                foreach (var ll in lls)
//                {
//                    ll.Height = height;
//                }
//            }
//        }

//        int ComputeWall(PositionInfo[,] heightMap, int xLength, int yLength, int? minHeight,
//            ISet<PositionInfo> computing,
//            ISet<PositionInfo> edges,
//            IList<PositionInfo> lowLying)
//        {
//            foreach (var ll in lowLying)
//            {
//                edges.Add(heightMap[ll.X, ll.Y - 1]);
//                edges.Add(heightMap[ll.X, ll.Y + 1]);
//                edges.Add(heightMap[ll.X - 1, ll.Y]);
//                edges.Add(heightMap[ll.X + 1, ll.Y]);

//                computing.Add(ll);
//            }

//            return ComputePoint(heightMap, xLength, yLength, minHeight, computing, edges, lowLying);
//        }

//        int ComputePoint(PositionInfo[,] heightMap, int xLength, int yLength, int? minHeight,
//            ISet<PositionInfo> computing,
//            ISet<PositionInfo> edges,
//            IList<PositionInfo> lowLying)
//        {
//            while (true)
//            {
//                var min = edges.Where(p => !computing.Contains(p)).OrderBy(p => p.Value).FirstOrDefault();
//                if (min == null) return int.MaxValue;

//                if (minHeight.HasValue && min.Value >= minHeight) return minHeight.Value;

//                var height = ComputeHeight(heightMap, xLength, yLength, min, computing, edges, lowLying);
//                if (height > 0)
//                {
//                    if (height > minHeight.GetValueOrDefault(height))
//                    {
//                        foreach (var ll in lowLying)
//                        {
//                            if (ll.Height > 0) throw new Exception($"高错误：{ll}:{ll.Height}");

//                            ll.Height = height;
//                        }
//                    }

//                    return height;
//                }
//            }
//        }

//        int ComputeHeight(PositionInfo[,] heightMap, int xLength, int yLength, PositionInfo position,
//            ISet<PositionInfo> computing,
//            ISet<PositionInfo> edges,
//            IList<PositionInfo> lowLying)
//        {
//            if (position.X < 1 || position.Y < 1 || position.X > xLength - 2 || position.Y > yLength - 2) return position.Value;

//            computing.Add(position);

//            if (position.Height > 0) return position.Height;

//            var list = new List<int>();
//            var e1 = new SortedSet<PositionInfo>();
//            var l1 = new List<PositionInfo>();

//            foreach (var p in new[]
//            {
//                heightMap[position.X, position.Y - 1],
//                heightMap[position.X, position.Y + 1],
//                heightMap[position.X + 1, position.Y],
//                heightMap[position.X - 1, position.Y]
//            })
//            {
//                if (computing.Contains(p)) continue;

//                if (position.Value <= p.Value)
//                {
//                    edges.Add(p);
//                }
//                else
//                {
//                    if (p.X < 1 || p.Y < 1 || p.X > xLength - 2 || p.Y > yLength - 2)
//                    {
//                        if (position.Value > p.Value) list.Add(position.Value);
//                    }
//                    else
//                    {
//                        var e2 = new SortedSet<PositionInfo>();
//                        var l2 = new List<PositionInfo> { p };

//                        if (position.Value > ComputeWall(heightMap, xLength, yLength, position.Value, computing, e2, l2))
//                        {
//                            list.Add(position.Value);
//                        }
//                        else
//                        {
//                            foreach (var a in e2) e1.Add(a);
//                            foreach (var a in l2) l1.Add(a);
//                        }
//                    }
//                }
//            }

//            if (list.Count > 0) return list.Min();

//            foreach (var a in e1) edges.Add(a);
//            foreach (var a in l1) lowLying.Add(a);

//            lowLying.Add(position);
//            edges.Remove(position);

//            return -1;
//        }

//        class PositionInfo : IComparable<PositionInfo>, IComparable
//        {
//            public PositionInfo(int value, int x, int y)
//            {
//                Value = value;
//                X = x;
//                Y = y;
//            }

//            public int Value { get; }
//            public int X { get; }
//            public int Y { get; }
//            public int Height { get; set; }

//            public override string ToString() => $"{X},{Y},{Value}";

//            public int CompareTo(PositionInfo other)
//            {
//                if (ReferenceEquals(this, other)) return 0;
//                if (ReferenceEquals(null, other)) return 1;
//                var xComparison = X.CompareTo(other.X);
//                if (xComparison != 0) return xComparison;
//                return Y.CompareTo(other.Y);
//            }

//            public int CompareTo(object obj)
//            {
//                if (ReferenceEquals(null, obj)) return 1;
//                if (ReferenceEquals(this, obj)) return 0;
//                return obj is PositionInfo other ? CompareTo(other) : throw new ArgumentException($"Object must be of type {nameof(PositionInfo)}");
//            }

//            public override bool Equals(object obj) => CompareTo(obj) == 0;

//            public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
//        }

//        private void Dump(PositionInfo[,] map, int numberLength, int xLength, int yLength)
//        {
//            var sb = new StringBuilder();

//            for (var x = 0; x < xLength; x++)
//            {
//                if (x > 0) sb.Append(',').AppendLine();

//                sb.Append('{');
//                for (var y = 0; y < yLength; y++)
//                {
//                    var p = map[x, y];

//                    if (y > 0) sb.Append(", ");

//                    sb.AppendFormat($"{{0,{numberLength}}}", p.Value).Append(':').AppendFormat($"{{0,{numberLength}}}", p.Height);
//                }

//                sb.Append('}');
//            }

//            _logger.LogInformation(sb.ToString());
//        }

//        private void Dump2(PositionInfo[,] map, int numberLength, int xLength, int yLength)
//        {
//            var sb = new StringBuilder();
//            foreach (var a in FindLowLying(map, xLength, yLength))
//            {
//                if (sb.Length > 0) sb.AppendLine();

//                var first = false;
//                foreach (var b in a)
//                {
//                    if (first) sb.Append(" ,");
//                    first = true;
//                    sb.Append(b.X).Append(':').Append(b.Y).Append(':').AppendFormat($"{{0,{numberLength}}}", b.Value);
//                }
//            }

//            _logger.LogInformation(sb.ToString());
//        }
//    }
//}
