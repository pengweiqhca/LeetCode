﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode.P1
{
    public class LeetCode174
    {
        public int CalculateMinimumHP(int[][] dungeon)
        {
            int xLength = dungeon.Length, yLength = dungeon.Max(m => m.Length);

            return CalculateMinimumHp(dungeon, new int[xLength, yLength], xLength, yLength, 0, 0);
        }

        private static int CalculateMinimumHp(IReadOnlyList<IReadOnlyList<int>> dungeon, int[,] minHp, int xLength, int yLength, int x, int y) =>
            minHp[x, y] > 0
                ? minHp[x, y]
                : minHp[x, y] = Math.Max(1, (x == xLength - 1
                    ? y == yLength - 1 ? 1 : CalculateMinimumHp(dungeon, minHp, xLength, yLength, x, y + 1)
                    : y == yLength - 1
                        ? CalculateMinimumHp(dungeon, minHp, xLength, yLength, x + 1, y)
                        : Math.Min(CalculateMinimumHp(dungeon, minHp, xLength, yLength, x, y + 1), CalculateMinimumHp(dungeon, minHp, xLength, yLength, x + 1, y))) - dungeon[x][y]);
    }
}
