using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// An action which waits for a trigger to return true before executing.
    /// </summary>
    public sealed class BowAction : IAction
    {
        private readonly Func<bool> trigger;
        private readonly Action prepare;
        private readonly Action shoot;
        private readonly TimeSpan timeout;

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action shoot) : this(trigger, () => { }, shoot, new TimeSpan(0, 0, 5))
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action shoot, TimeSpan timeout) : this(trigger, () => { }, shoot, timeout)
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action prepare, Action shoot) : this(trigger, prepare, shoot, new TimeSpan(0, 0, 5))
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action prepare, Action shoot, TimeSpan timeout)
        {
            this.trigger = trigger;
            this.prepare = prepare;
            this.shoot = shoot;
            this.timeout = timeout;
        }

        public void Invoke()
        {
            this.prepare();
            var timeout = DateTime.Now + this.timeout;
            var completed = false;

            var parallel =
                new Task(() =>
                    {
                        while (true)
                        {
                            if (this.trigger.Invoke())
                            {
                                this.shoot();
                                completed = true;
                                break;
                            }
                            System.Threading.Thread.Sleep(1);
                        }
                    }
                );
            parallel.Start();
            parallel.ContinueWith((t) =>
                {
                    throw t.Exception.InnerException;
                },
                TaskContinuationOptions.OnlyOnFaulted
            );

            while(DateTime.Now < timeout)
            {
                if (completed) break;
            }

            if (!completed)
            {
                throw new ApplicationException($"The task did not complete within {this.timeout.TotalMilliseconds}ms.");
            }
        }
    }

    /// <summary>
    /// An action which waits for a trigger to return true before executing.
    /// </summary>
    public sealed class BowFunc<T> : IAction<T>
    {
        private readonly Func<bool> trigger;
        private readonly Action prepare;
        private readonly Action<T> shoot;
        private readonly TimeSpan timeout;

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowFunc(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout)
        {
            this.trigger = trigger;
            this.prepare = prepare;
            this.shoot = shoot;
            this.timeout = timeout;
        }

        public void Invoke(T parameter)
        {
            this.prepare();
            var timeout = DateTime.Now + this.timeout;
            var completed = false;

            var parallel =
                new Task(() =>
                {
                    while (true)
                    {
                        if (this.trigger.Invoke())
                        {
                            this.shoot(parameter);
                            completed = true;
                            break;
                        }
                        System.Threading.Thread.Sleep(1);
                    }
                }
                );
            parallel.Start();
            parallel.ContinueWith((t) =>
            {
                throw t.Exception.InnerException;
            },
                TaskContinuationOptions.OnlyOnFaulted
            );

            while (DateTime.Now < timeout)
            {
                if (completed) break;
            }

            if (!completed)
            {
                throw new ApplicationException($"The task did not complete within {this.timeout.TotalMilliseconds}ms.");
            }
        }
    }
}