using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms
{
    public sealed class AndInThreads<T> : IScalar<bool>
    {
        private IEnumerable<IScalar<bool>> iterable;

        public AndInThreads(params IScalar<bool>[] src) : this(
            new ManyOf<IScalar<bool>>(src)
        )
        { }


        public AndInThreads(IAction<T> proc, params T[] src) : this(
            new FuncOf<T,bool>(proc, true), src
        )
        { }


        public AndInThreads(IFunc<T, bool> func, params T[] src) : this(
            func, new ManyOf<T>(src)
        )
        { }


        public AndInThreads(IAction<T> proc, IEnumerable<T> src) : this(
            new FuncOf<T,bool>(proc, true), src
        )
        { }

        public AndInThreads(IFunc<T, bool> func, IEnumerable<T> src) : this(
            new Mapped<T, IScalar<bool>>(
                i => new ScalarOf<bool>(func.Invoke(i)),
                src)
            )
        {}

        public AndInThreads(IEnumerable<IScalar<bool>> src)
        {
            this.iterable = src;
        }

        public bool Value()
        {
            var result = true;

            Parallel.ForEach(this.iterable, test =>
            {
                if (!test.Value())
                {
                    result = false;
                }
            });

            return result;
        }
        
    }
}
