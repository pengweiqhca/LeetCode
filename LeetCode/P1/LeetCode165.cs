using System;

namespace LeetCode.P1
{
    public class LeetCode165
    {
        public int CompareVersion(string version1, string version2)
        {
            var index1 = 0;
            var index2 = 0;

            while (index1 < version1.Length || index2 < version2.Length)
            {
                var num1 = index1 < version1.Length ? GetNumber(version1.AsSpan(index1), ref index1) : 0;
                var num2 = index2 < version2.Length ? GetNumber(version2.AsSpan(index2), ref index2) : 0;
                if (num1 < num2) return -1;
                if (num1 > num2) return 1;
            }

            return 0;
        }

        private static int GetNumber(ReadOnlySpan<char> version, ref int index2)
        {
            var i = version.IndexOf('.');

            if (version.Length == 0) return 0;

            if (i == 0)
            {
                index2++;
                return -1;
            }

            if (i > 0)
            {
                index2 += i + 1;
                version = version.Slice(0, i);
            }
            else index2 += version.Length;

            var num = 0;

            foreach (var ch in version)
            {
                num = num * 10 + (ch - '0');
            }

            return num;
        }
    }
}
