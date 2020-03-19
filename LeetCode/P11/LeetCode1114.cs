using System;
using System.Threading;

namespace LeetCode.P11
{
    public class LeetCode1114
    {
        private readonly AutoResetEvent _oddLock = new AutoResetEvent(false);
        private readonly AutoResetEvent _evenLock = new AutoResetEvent(false);

        public void First(Action printFirst)
        {
            // printFirst() outputs "first". Do not change or remove this line.
            printFirst();
            _evenLock.Reset();
        }

        public void Second(Action printSecond)
        {
            // printSecond() outputs "second". Do not change or remove this line.
            if (_evenLock.WaitOne()) printSecond();
            _oddLock.Reset();
        }

        public void Third(Action printThird)
        {
            // printThird() outputs "third". Do not change or remove this line.
            if (_evenLock.WaitOne()) printThird();
        }
    }
}
