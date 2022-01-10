// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
            new TimeSpan(0, 0, 10)
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
            new TimeSpan(0, 0, 0, 0, 250)
        )
        { }

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public BowFunc(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout, TimeSpan interval) : this(
            trigger,
            prepare,
            shoot,
            new MapOf<TimeSpan>(
                new KvpOf<TimeSpan>("timeout", timeout),
                new KvpOf<TimeSpan>("interval", interval)
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
                        Task.Delay(this.timespans["interval"]).Wait();
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

    public static class BowFunc
    {
        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public static BowFunc<T> New<T>(Func<bool> trigger, Action<T> shoot)
            => new BowFunc<T>(trigger, shoot);

        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public static BowFunc<T> New<T>(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout)
            => new BowFunc<T>(trigger, prepare, shoot, timeout);


        /// <summary>
        /// A Function which waits for a trigger to return true before executing.
        /// </summary>
        public static BowFunc<T> New<T>(Func<bool> trigger, Action prepare, Action<T> shoot, TimeSpan timeout, TimeSpan interval)
            => new BowFunc<T>(trigger, prepare, shoot, timeout, interval);

    }
}
