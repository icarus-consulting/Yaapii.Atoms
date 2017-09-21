using System.IO;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A <see cref="IScalar{T}"/> that doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class UncheckedScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _origin;

        /// <summary>
        /// A <see cref="IScalar{T}"/> that doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead
        /// </summary>
        /// <param name="fnc">value retrieving function to uncheck</param>
        public UncheckedScalar(System.Func<T> fnc) : this(new ScalarOf<T>(() => fnc.Invoke()))
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead
        /// </summary>
        /// <param name="origin">scalar to call</param>
        public UncheckedScalar(IScalar<T> origin)
        {
            this._origin = origin;
        }

        public T Value()
        {
            try
            {
                return new IoCheckedScalar<T>(this._origin).Value();
            }
            catch (IOException ex)
            {
                throw new UncheckedIOException(ex);
            }
        }
    }
}
