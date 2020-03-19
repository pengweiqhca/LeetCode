namespace LeetCode.P0
{
    public partial class LeetCode19
    {
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head?.next == null && n > 0) return null;

            var current = head;
            while (current.next != null && n-- > 1)
            {
                current = current.next;
            }

            if (current.next == null) return n > 1 ? head : head.next;

            var deleted = head;
            current = current.next;
            while (current.next != null)
            {
                current = current.next;
                deleted = deleted.next;
            }

            deleted.next = deleted.next.next;

            return head;
        }
    }
}
