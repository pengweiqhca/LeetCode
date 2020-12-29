using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.P0
{
    public class LeetCode85
    {
        public int MaximalRectangle(char[][] matrix)
        {

        }

        private struct Coordinate
        {
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }

        private IEnumerable<int> ComputeRectangle(char[][] matrix, Coordinate length)
        {
            for (var x = 0; x < xLength; x++)
                for (var y = 0; y < yLength; y++)
                {
                    var a = new int[10, 10];
                }
        }

        private IEnumerable<Coordinate> FindRectangle(char[][] matrix, Coordinate length, Coordinate lt, Coordinate rb)
        {

        }
    }
}
