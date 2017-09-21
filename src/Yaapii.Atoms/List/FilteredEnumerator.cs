using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.List
{
#pragma warning disable NoProperties // No Properties

    /// <summary>
    /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class FilteredEnumerator<X> : IEnumerator<X>
    {
        /// <summary>
        /// enumerator to filter
        /// </summary>
        private readonly IEnumerator<X> _enumerator;

        /// <summary>
        /// filter function
        /// </summary>
        private readonly IFunc<X, Boolean> _func;

        /// <summary>
        /// buffer to store filtered elements
        /// </summary>
        private readonly Queue<X> _buffer;

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public FilteredEnumerator(IEnumerator<X> src, Func<X, Boolean> fnc) : this(src, new FuncOf<X, Boolean>(fnc))
        { }

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public FilteredEnumerator(IEnumerator<X> src, IFunc<X, Boolean> fnc)
        {
            this._enumerator = src;
            this._func = fnc;
            this._buffer = new Queue<X>();
        }

        public X Current
        {
            get
            {
                return this._buffer.Peek();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this._buffer.Peek();
            }
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            UncheckedFunc<X, Boolean> fnc = new UncheckedFunc<X, Boolean>(this._func);
            if (this._buffer.Count > 0) this._buffer.Dequeue();

            if (this._buffer.Count == 0)
            {
                while (this._enumerator.MoveNext())
                {
                    X obj = this._enumerator.Current;
                    if (fnc.Invoke(obj))
                    {
                        this._buffer.Enqueue(obj);
                        break;
                    }
                }
            }
            else
            {
                this._buffer.Dequeue();
            }

            return this._buffer.Count > 0;
        }

        public void Reset()
        {
            this._enumerator.Reset();
        }
    }
}
#pragma warning restore NoProperties // No Properties
