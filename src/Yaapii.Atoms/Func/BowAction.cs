// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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
            var completed = false;

            var parallel =
                new Task(() =>
                {
                    while (true)
                    {
                        lock (this.timespans)
                        {
                            if (this.trigger.Invoke())
                            {
                                this.actions["shoot"]();
                                completed = true;
                                break;
                            }
                            Task.Delay(this.timespans["interval"]).Wait();
                        }
                    }
                }
                );
            try
            {
                parallel.Start();
                parallel.Wait(this.timespans["timeout"]);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }

            if (parallel.Status == TaskStatus.Faulted)
            {
                throw parallel.Exception.InnerException;
            }

            if (!completed)
            {
                throw new ApplicationException($"The task did not complete within {this.timespans["timeout"].TotalMilliseconds}ms.");
            }
        }
    }
}
