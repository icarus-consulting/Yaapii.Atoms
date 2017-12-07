using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
    /// </summary>
    /// <typeparam name="T">Type of the enumerable</typeparam>
    public sealed class NotEmpty<T> : EnumerableEnvelope<T>
    {
        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        public NotEmpty(IEnumerable<T> origin) : base(new ScalarOf<IEnumerable<T>>(
            () =>
            {
                new FailEmpty<T>(origin).Go();
                return origin;
            }
            ))
        { }
    }
}
