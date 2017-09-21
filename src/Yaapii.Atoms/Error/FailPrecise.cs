using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail with changed exception.
    /// </summary>
    public sealed class FailPrecise : IFail
    {
        private readonly IFail _origin;
        private readonly Exception _precision;

        /// <summary>
        /// Decorates a Failing by replacing the thrown Exception with the injected one
        /// </summary>
        /// <param name="origin">The Failing to decorate</param>
        /// <param name="preciseException">The new Exception to throw when necessary</param>
        public FailPrecise(IFail origin, Exception precision)
        {
            _origin = origin;
            _precision = precision;
        }

        public void Go()
        {
            try
            {
                _origin.Go();
            }
            catch (Exception)
            {
                throw _precision;
            }
        }
    }
}
