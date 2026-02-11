using System;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Executes a function if the input is not null, otherwise returns a fallback value.
    /// </summary>
    public sealed class DoIfNotNull<In, Result> : ScalarEnvelope<Result>
    {
        public DoIfNotNull(In input, Result fallback, Func<In, Result> continued) : this(
            () => input,
            fallback,
            continued
        )
        { }

        /// <summary>
        /// Executes a function if the input is not null, otherwise returns a fallback value.
        /// </summary>
        public DoIfNotNull(Func<In> input, Result fallback, Func<In, Result> continued) : base(() =>
        {
            var result = fallback;

            var prep = input.Invoke();
            if (prep != null)
            {
                result = continued.Invoke(prep);
            }

            return result;
        })
        { }
    }

    public static class DoIfNotNull
    {
        /// <summary>
        /// Executes a function if the input is not null, otherwise returns a fallback value.
        /// </summary>
        public static IScalar<Result> New<In, Result>(In input, Result fallback, Func<In, Result> continued) =>
            new DoIfNotNull<In, Result>(input, fallback, continued);

        /// <summary>
        /// Executes a function if the input is not null, otherwise returns a fallback value.
        /// </summary>
        public static IScalar<Result> New<In, Result>(Func<In> input, Result fallback, Func<In, Result> continued) =>
            new DoIfNotNull<In, Result>(input, fallback, continued);
    }
}
