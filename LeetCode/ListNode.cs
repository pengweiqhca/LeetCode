namespace LeetCode
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }

        public override string ToString() => next == null ? val.ToString() : $"{val}, {next}";
    }
}
