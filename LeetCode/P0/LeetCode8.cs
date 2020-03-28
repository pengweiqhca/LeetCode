namespace LeetCode.P0
{
    public class LeetCode8
    {
        public int MyAtoi(string str)
        {
            var num = 0l;
            var state = 0;
            foreach (var ch in str)
            {
                if (ch == ' ')
                {
                    if (state == 0) continue;
                    break;
                }

                if (ch == '+' || ch == '-')
                {
                    if (state == 0)
                    {
                        state = ch == '+' ? 1 : -1;
                        continue;
                    }

                    break;
                }

                if (ch < '0' || ch > '9') break;

                if (state == 0) state = 1;
                num = num * 10 + (ch - '0');
                if (num > int.MaxValue) break;
            }

            if (num > int.MaxValue) return state < 0 ? int.MinValue : int.MaxValue;

            return state < 0 ? -(int)num : (int)num;
        }
    }
}
