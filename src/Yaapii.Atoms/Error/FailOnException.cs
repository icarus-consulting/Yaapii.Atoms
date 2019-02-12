using System;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fails with given exception if the invoked action fails
    /// </summary>
    /// <typeparam name="T">The expected Exception type</typeparam>
    public sealed class FailOnException<T> : IFail
    {
        private readonly IAction action;
        private readonly Exception exception;

        /// <summary>
        /// Fails with given exception if the invoked action fails
        /// </summary>
        /// <typeparam name="T">The expected Exception type</typeparam>
        public FailOnException(Action action, Exception exception) : this(
            new ActionOf(action),
            exception)
        { }

        /// <summary>
        /// Fails with given exception if the invoked action fails
        /// </summary>
        /// <typeparam name="T">The expected Exception type</typeparam>
        public FailOnException(IAction action, Exception exception)
        {
            this.action = action;
            this.exception = exception;
        }

        public void Go()
        {
            try
            {
                this.action.Invoke();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(T))
                {
                    throw exception;
                }

                throw ex;
            }
        }
    }
}
