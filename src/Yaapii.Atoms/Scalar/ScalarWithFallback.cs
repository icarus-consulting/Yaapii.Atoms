using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Scalar which calls a fallback function if Value() fails.
    /// </summary>
    /// <typeparam name="Out">Type of output value</typeparam>
    public class ScalarWithFallback<Out> : IScalar<Out>
    {
        private readonly IScalar<Out> _origin;
        private readonly Func<Exception, Out> _fallback;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public ScalarWithFallback(IScalar<Out> origin, Out fallback) : this(
            origin, 
            (ex) => fallback)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public ScalarWithFallback(IScalar<Out> origin, Func<Out> fallback) : this(
            origin, 
            (ex) => fallback.Invoke())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">scalar which can fail</param>
        /// <param name="fbk">fallback to apply when fails</param>
        public ScalarWithFallback(IScalar<Out> origin, Func<Exception, Out> fbk)
        {
            _origin = origin;
            _fallback = fbk;
        }

        /// <summary>
        /// Get the value or fallback if fails
        /// </summary>
        /// <returns>value or fallback value</returns>
        public Out Value()
        {
            try
            {
                return _origin.Value();
            }
            catch (Exception ex)
            {
                return _fallback.Invoke(ex);
            }

        }
    }
}
