using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Tests
{
    public sealed class ElapsedTime
    {
        private readonly Action _work;

        public ElapsedTime(Action work)
        {
            _work = work;
        }

        public TimeSpan AsTimeSpan()
        {
            var start = DateTime.Now;
            _work.Invoke();
            return DateTime.Now - start;
        }
    }
}
