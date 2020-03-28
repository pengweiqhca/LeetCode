using System;

namespace LeetCode.P0
{
    public class LeetCode65
    {
        public bool IsNumber(string s)
        {
            if (s.Length == 0) return true;

            var span = s.AsSpan().Trim();
            if (span.Length == 0) return false;

            return IsNumber(span, out var exponent) &&
                   (exponent < 0 || exponent < span.Length && IsExponent(span.Slice(exponent)));
        }

        private static bool IsNumber(ReadOnlySpan<char> s, out int exponent)
        {
            exponent = 0;

            var state = 0;
            while (exponent < s.Length)
            {
                var ch = s[exponent++];
                if (ch == '+' || ch == '-')
                {
                    if (state > 0) return false;

                    state = 1;
                    continue;
                }

                if (ch >= '0' && ch <= '9')
                {
                    state |= 4;
                    continue;
                }

                if (ch == '.')
                {
                    if ((state & 2) == 2) return false;
                    state |= 2;
                }
                else
                    return ch == 'e' && (state & 4) == 4;
            }

            exponent = -1;
            return (state & 4) == 4;
        }

        public static bool IsExponent(ReadOnlySpan<char> s)
        {
            var state = 0;

            foreach (var ch in s)
            {
                if (ch == '+' || ch == '-')
                {
                    if (state > 0) return false;

                    state = 1;
                    continue;
                }

                if (ch < '0' || ch > '9') return false;

                state = 2;
            }

            return state > 1;
        }
    }
}
