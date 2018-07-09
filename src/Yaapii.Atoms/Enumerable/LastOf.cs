using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Enumerable
{
    public sealed class LastOf<T> : IScalar<T>
    {
        private readonly IEnumerable<T> src;

        public LastOf(IEnumerable<T> src)
        {
            this.src = src;
        }
        public T Value()
        {
            return
                new ItemAt<T>(
                    new Reversed<T>(this.src)
                ).Value();
        }
    }
}
