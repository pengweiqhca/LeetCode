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

            ComputeX(height, xLength, yLength);
            ComputeY(height, xLength, yLength);
            Dump(height, xLength, yLength);
            ComputeHeight(height, xLength, yLength);
            Dump(height, xLength, yLength);

            var total = 0;

            for (var x = 1; x < xLength - 1; x++)
                for (var y = 1; y < yLength - 1; y++)
                {
                    var position = height[x, y];
                    total += Math.Max(0, position.Height - position.Value);
                }

            return total;
        }

        private static void ComputeX(PositionInfo[,] height, int xLength, int yLength)
        {
            for (var x = 0; x < xLength; x++)
            {
                int start = -1, end = -1;

                for (var y = 0; y < yLength; y++)
                {
                    if (start < 0)
                    {
                        if (height[x, y].Value > 0) start = y;
                    }
                    else if (height[x, y].Value >= height[x, start].Value)
                    {
                        for (var index = start + 1; index < y; index++)
                        {
                            height[x, index].XHeight = height[x, start].Value;
                        }

                        start = y;
                    }
                }

                if (start < 0) return;

                for (var y = yLength - 1; y >= start; y--)
                {
                    if (end < 0)
                    {
                        if (height[x, y].Value > 0) end = y;
                    }
                    else if (height[x, y].Value >= height[x, end].Value)
                    {
                        for (var index = y + 1; index < end; index++)
                        {
                            height[x, index].XHeight = height[x, end].Value;
                        }

                        end = y;
                    }
                }
            }
        }

        private static void ComputeY(PositionInfo[,] height, int xLength, int yLength)
        {
            for (var y = 0; y < yLength; y++)
            {
                int start = -1, end = -1;

                for (var x = 0; x < xLength; x++)
                {
                    if (start < 0)
                    {
                        if (height[x, y].Value > 0) start = x;
                    }
                    else if (height[x, y].Value >= height[start, y].Value)
                    {
                        for (var index = start + 1; index < x; index++)
                        {
                            height[index, y].YHeight = height[start, y].Value;
                        }

                        start = x;
                    }
                }

                if (start < 0) return;

                for (var x = xLength - 1; x >= start; x--)
                {
                    if (end < 0)
                    {
                        if (height[x, y].Value > 0) end = x;
                    }
                    else if (height[x, y].Value >= height[end, y].Value)
                    {
                        for (var index = x + 1; index < end; index++)
                        {
                            height[index, y].YHeight = height[end, y].Value;
                        }

                        end = x;
                    }
                }
            }
        }

        private static void ComputeHeight(PositionInfo[,] height, int xLength, int yLength)
        {
            bool hasUpdated;
            do
            {
                hasUpdated = false;

                for (var x = 1; x < xLength - 1; x++)
                {
                    for (var y = 1; y < yLength - 1; y++)
                    {
                        var position = height[x, y];
                        if (!IsPool(position)) continue;

                        var area = new List<PositionInfo> { position };
                        var cache = new HashSet<PositionInfo>();

                        var length = 0;
                        while (length < area.Count)
                        {
                            var rawLength = area.Count;

                            for (var index = length; index < rawLength; index++)
                            {
                                position = area[index];

                                if (position.X > 1) TryAdd(height[position.X - 1, position.Y], area, cache);
                                if (position.X < xLength - 1) TryAdd(height[position.X + 1, position.Y], area, cache);
                                if (position.Y > 1) TryAdd(height[position.X, position.Y - 1], area, cache);
                                if (position.Y < yLength - 1) TryAdd(height[position.X, position.Y + 1], area, cache);
                            }

                            length = rawLength;
                        }

                        var min = area.Min(p =>
                        {
                            var v = p.Height;
                            position = height[p.X - 1, p.Y];
                            if (!cache.Contains(position)) v = Math.Min(position.Value, v);
                            position = height[p.X + 1, p.Y];
                            if (!cache.Contains(position)) v = Math.Min(position.Value, v);
                            position = height[p.X, p.Y - 1];
                            if (!cache.Contains(position)) v = Math.Min(position.Value, v);
                            position = height[p.X, p.Y + 1];
                            if (!cache.Contains(position)) v = Math.Min(position.Value, v);

                            if (v <= p.Value)
                            {
                                p.Height = 0;
                                hasUpdated = true;
                            }
                            if (v < p.Height)
                            {
                                p.Height = v;
                                hasUpdated = true;
                            }

                            return v;
                        });

                        if (hasUpdated) break;

                        if (area.Count > 1)
                        {
                            var processed = new HashSet<PositionInfo>();
                            hasUpdated = Sync(height, processed, area.First(p => p.Height == min));
                            if (hasUpdated) break;
                        }
                    }

                    if (hasUpdated) break;
                }
            } while (hasUpdated);
        }

        private static bool Sync(PositionInfo[,] height, ISet<PositionInfo> processed, PositionInfo position) =>
            Sync2(height, processed, height[position.X - 1, position.Y], position) ||
            Sync2(height, processed, height[position.X + 1, position.Y], position) ||
            Sync2(height, processed, height[position.X, position.Y - 1], position) ||
            Sync2(height, processed, height[position.X, position.Y + 1], position);

        private static bool Sync2(PositionInfo[,] height, ISet<PositionInfo> processed, PositionInfo position1, PositionInfo position2)
        {
            if (processed.Contains(position1) || !IsPool(position1) || position1.Height <= position2.Height) return false;

            processed.Add(position1);

            if (position1.Value >= position2.Height)
            {
                position1.Height = 0;
                return false;
            }

            position1.Height = position2.Height;

            return Sync(height, processed, position1);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static void TryAdd(PositionInfo position, ICollection<PositionInfo> area, ISet<PositionInfo> cache)
        {
            if (IsPool(position) && cache.Add(position)) area.Add(position);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private static bool IsPool(PositionInfo position) => position.XHeight > 0 && position.YHeight > 0;

        class PositionInfo
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
            public int XHeight { get; set; }
            public int YHeight { get; set; }

            public int Height
            {
                get => XHeight > 0 && YHeight > 0 ? Math.Min(XHeight, YHeight) : 0;
                set => XHeight = YHeight = value;
            }

            public override string ToString() => Value.ToString();
        }

        private void Dump(PositionInfo[,] map, int xLength, int yLength)
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

                    sb.AppendFormat("{0,5}", p.Value).Append(':').AppendFormat("{0,5}", p.Height);
                }

                sb.Append('}');
            }

            _logger.LogInformation(sb.ToString());
        }
    }
}
