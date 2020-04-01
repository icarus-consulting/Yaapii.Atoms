using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// An action which waits for a trigger to return true before executing.
    /// </summary>
    public sealed class BowAction : IAction
    {
        private readonly Func<bool> trigger;
        private readonly IDictionary<string, Action> actions;
        private readonly IDictionary<string, TimeSpan> timespans;

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action shoot) : this(
            trigger, () => { },
            shoot,
            new TimeSpan(0, 0, 10),
            new TimeSpan(0, 0, 0, 0, 250)
        )
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action shoot, TimeSpan timeout) : this(
            trigger,
            () => { },
            shoot,
            timeout,
            new TimeSpan(0, 0, 0, 0, 250)
        )
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        public BowAction(Func<bool> trigger, Action prepare, Action shoot) : this(
            trigger,
            prepare,
            shoot,
            new TimeSpan(0, 0, 10),
            new TimeSpan(0, 0, 0, 0, 250)
        )
        { }

        public BowAction(Func<bool> trigger, Action prepare, Action shoot, TimeSpan timeout, TimeSpan interval) : this(
            trigger,
            new MapOf<Action>(
                new KvpOf<Action>("prepare", prepare),
                new KvpOf<Action>("shoot", shoot)
            ),
            new MapOf<TimeSpan>(
                new KvpOf<TimeSpan>("timeout", timeout),
                new KvpOf<TimeSpan>("interval", interval)
            ))
        { }

        /// <summary>
        /// An action which waits for a trigger to return true before executing.
        /// </summary>
        private BowAction(Func<bool> trigger, IDictionary<string, Action> actions, IDictionary<string, TimeSpan> timespans)
        {
            this.trigger = trigger;
            this.actions = actions;
            this.timespans = timespans;
        }

        public void Invoke()
        {
            this.actions["prepare"]();
            var timeout = DateTime.Now + this.timespans["timeout"];
            var completed = false;

            var parallel =
                new Task(() =>
                {
                    while (true)
                    {
                        if (this.trigger.Invoke())
                        {
                            this.actions["shoot"]();
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
                if (parallel.Status == TaskStatus.Faulted)
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