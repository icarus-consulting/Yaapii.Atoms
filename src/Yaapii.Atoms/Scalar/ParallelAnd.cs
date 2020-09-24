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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Logical conjunction, in multiple threads. Returns true if all contents return true.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ParallelAnd<T> : IScalar<bool>
    {
        private IEnumerable<IScalar<bool>> iterable;

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public ParallelAnd(params IScalar<bool>[] src) : this(
            new ManyOf<IScalar<bool>>(src)
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public ParallelAnd(IAction<T> proc, params T[] src) : this(
            new FuncOf<T,bool>(proc, true), src
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public ParallelAnd(IFunc<T, bool> func, params T[] src) : this(
            func, new ManyOf<T>(src)
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="src"></param>
        public ParallelAnd(IAction<T> proc, IEnumerable<T> src) : this(
            new FuncOf<T,bool>(proc, true), src
        )
        { }

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="src"></param>
        public ParallelAnd(IFunc<T, bool> func, IEnumerable<T> src) : this(
            new Mapped<T, IScalar<bool>>(
                i => new ScalarOf<bool>(func.Invoke(i)),
                src)
            )
        {}

        /// <summary>
        /// Logical conjunction, in multiple threads. Returns true if all contents return true.
        /// </summary>
        /// <param name="src"></param>
        public ParallelAnd(IEnumerable<IScalar<bool>> src)
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
