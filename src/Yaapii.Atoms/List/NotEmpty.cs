using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Ensures that <see cref="IList{T}" /> is not empty/>
    /// </summary>
    /// <typeparam name="T">Type of the list</typeparam>
    public sealed class NotEmpty<T> : ListEnvelope<T>
    {
        /// <summary>
        /// Ensures that <see cref="IList{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">List</param>
        public NotEmpty(IList<T> origin) : base(new ScalarOf<IList<T>>(
            () =>
            {
                new FailEmpty<T>(origin).Go();
                return origin;
            }
            ))
        { }
    }
}
