using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// Ensures that <see cref="ICollection{T}" /> is not empty/>
    /// </summary>
    /// <typeparam name="T">Type of the collection</typeparam>
    public sealed class NotEmpty<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Collection</param>
        public NotEmpty(ICollection<T> origin) : base(new ScalarOf<ICollection<T>>(
            () =>
            {
                new FailEmpty<T>(origin).Go();
                return origin;
            }
            ))
        { }
    }
}
