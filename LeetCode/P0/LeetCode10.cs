namespace LeetCode.P0
{
    public class LeetCode10
    {
        public bool IsMatch(string s, string p)
        {
            if (p.Length == 0) return s.Length == 0;

            var pi = 0;
            while (pi < p.Length)
            {
                if (p[pi] == '*') pi++;
                else break;
            }

            return MinMatch(p, pi, s, 0);
        }

        private static bool MinMatch(string p, int pi, string s, int si)
        {
            var ch = p[pi++];
            var multiple = false;
            var num = 1;

            while (pi < p.Length)
            {
                var c = p[pi];
                if (c == ch)
                {
                    num++;
                }
                else if (c == '*')
                {
                    multiple = true;
                    num--;
                }
                else break;

                pi++;
            }

            do
            {
                var index = si;
                if (!IsMatch(ch, num++, s, ref index)) return false;

                if (index == s.Length)
                {
                    if (pi == p.Length) return true;

                    if (MinMatch(p, pi, s, index)) return true;
                }
                else if (index < s.Length)
                {
                    if (pi < p.Length && MinMatch(p, pi, s, index)) return true;
                }
                else
                {
                    si = index;
                    break;
                }
            } while (multiple);

            return si >= s.Length && pi >= p.Length;
        }

        private static bool IsMatch(char ch, int num, string s, ref int si)
        {
            if (ch == '.')
            {
                si += num;

                return si <= s.Length;
            }

            while (si < s.Length && s[si] == ch && num-- > 0)
            {
                si++;
            }

            return num <= 0;
        }
    }
}
