using System;
using System.Threading;

namespace LeetCode.P11
{
    public class LeetCode1116
    {
        private readonly int _n;
        private readonly AutoResetEvent _zeroLock = new AutoResetEvent(true);
        private readonly AutoResetEvent _evenLock = new AutoResetEvent(false);
        private readonly AutoResetEvent _oddLock = new AutoResetEvent(false);
        private AutoResetEvent _lock;
        private int _number = 1;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public LeetCode1116(int n)
        {
            _n = n;
            _lock = _oddLock;
        }

        // printNumber(x) outputs "x", where x is an integer.
        public void Zero(Action<int> printNumber)
        {
            while (!_cts.IsCancellationRequested)
            {
                if (_zeroLock.WaitOne() && !_cts.IsCancellationRequested) printNumber(0);

                if (_lock == _evenLock)
                {
                    _evenLock.Set();
                    _lock = _oddLock;
                }
                else
                {
                    _oddLock.Set();
                    _lock = _evenLock;
                }
            }
        }

        public void Even(Action<int> printNumber) => Print(_evenLock, printNumber);

        public void Odd(Action<int> printNumber) => Print(_oddLock, printNumber);

        private void Print(WaitHandle @lock, Action<int> printNumber)
        {
            while (!_cts.IsCancellationRequested)
            {
                if (@lock.WaitOne() && !_cts.IsCancellationRequested) printNumber(_number);

                if (_number++ == _n) _cts.Cancel();

                _zeroLock.Set();
            }
        }
    }
}
