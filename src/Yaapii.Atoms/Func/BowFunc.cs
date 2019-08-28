using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// An Function which waits for a trigger to return true before executing.
    /// </summary>
    public sealed class BowFunc<T> : IAction<T>
    {
        private readonly Func<bool> trigger;
        private readonly Action prepare;
        private readonly Action<T> shoot;
        private readonly IDictionary<string, TimeSpan> timespans;

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public BowFunc(Func<bool> trigger, Action<T> shoot) : this(
            trigger,
            () => { },
            shoot,
            new TimeSpan(0,0,10)
        )
        { }

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public BowFunc(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout) : this(
            trigger,
            prepare,
            shoot,
            timeout,
            new TimeSpan(0,0,0,0,250)
        )
        { }

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public BowFunc(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout, TimeSpan interval) : this(
            trigger,
            prepare,
            shoot,
            new MapOf<string, TimeSpan>(
                new KeyValuePair<string, TimeSpan>("timeout", timeout),
                new KeyValuePair<string, TimeSpan>("interval", interval)
            ))
        { }

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        private BowFunc(Func<bool> trigger, Action prepare, Action<T> shoot, IDictionary<string, TimeSpan> timespans)
        {
            this.trigger = trigger;
            this.prepare = prepare;
            this.shoot = shoot;
            this.timespans = timespans;
        }

        public void Invoke(T parameter)
        {
            this.prepare();
            var timeout = DateTime.Now + this.timespans["timeout"];
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
                        System.Threading.Thread.Sleep(this.timespans["interval"]);
                    }
                }
                );
            parallel.Start();

            while (DateTime.Now < timeout)
            {
                if(parallel.Status == TaskStatus.Faulted)
                {
                    throw parallel.Exception.InnerException;
                }
                if (completed) break;
            }

            if (!completed)
            {
                throw new ApplicationException($"The task did not complete within {this.timespans["timeout"].TotalMilliseconds}ms.");
            }
        }
    }
}
