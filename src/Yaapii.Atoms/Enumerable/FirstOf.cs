using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Enumerable
{
    public sealed class FirstOf<T> : IScalar<T>
    {
        private readonly IEnumerable<T> src;

        public FirstOf(IEnumerable<T> src)
        {
            this.src = src;
        }
        public T Value()
        {
            return new ItemAt<T>(this.src).Value();
        }
    }
}
