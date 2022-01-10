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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// An action fork that is dependant on a named condition.
    /// </summary>
    public sealed class ActionSwitch<In> : IAction<string, In>
    {
        private readonly MapOf<Action<In>> map;
        private readonly Action<string, In> fallback;

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(params IKvp<Action<In>>[] consequences) : this(
            new ManyOf<IKvp<Action<In>>>(consequences)
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            IKvp<Action<In>> consequence7
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            IKvp<Action<In>> consequence7,
            IKvp<Action<In>> consequence8
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7,
                    consequence8
                ),
                (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            Action<string, In> fallback
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            Action<string, In> fallback
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            Action<string, In> fallback
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            Action<string, In> fallback
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            Action<string, In> fallback
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            IKvp<Action<In>> consequence7,
            Action<string, In> fallback
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In>> consequence1,
            IKvp<Action<In>> consequence2,
            IKvp<Action<In>> consequence3,
            IKvp<Action<In>> consequence4,
            IKvp<Action<In>> consequence5,
            IKvp<Action<In>> consequence6,
            IKvp<Action<In>> consequence7,
            IKvp<Action<In>> consequence8,
            Action<string, In> fallback
        ) : this(
                new ManyOf<IKvp<Action<In>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7,
                    consequence8
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IKvp<Action<In>>> consequences) : this(
            consequences,
            (unknown, input) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IKvp<Action<In>>> consequences, Action<string, In> fallback)
        {
            this.map = new MapOf<Action<In>>(consequences);
            this.fallback = fallback;
        }

        public void Invoke(string condition, In input)
        {
            if (!this.map.ContainsKey(condition))
            {
                this.fallback(condition, input);
            }
            else
            {
                this.map[condition].Invoke(input);
            }
        }
    }

    /// <summary>
    /// An action fork that is dependant on a named condition.
    /// </summary>
    public sealed class ActionSwitch<In1, In2> : IAction<string, In1, In2>
    {
        private readonly MapOf<Action<In1, In2>> map;
        private readonly Action<string, In1, In2> fallback;

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            IKvp<Action<In1, In2>> consequence8
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7,
                    consequence8
                ),
                (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            Action<string, In1, In2> fallback
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            Action<string, In1, In2> fallback
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            Action<string, In1, In2> fallback
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            Action<string, In1, In2> fallback
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            Action<string, In1, In2> fallback
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            Action<string, In1, In2> fallback
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(
            IKvp<Action<In1, In2>> consequence1,
            IKvp<Action<In1, In2>> consequence2,
            IKvp<Action<In1, In2>> consequence3,
            IKvp<Action<In1, In2>> consequence4,
            IKvp<Action<In1, In2>> consequence5,
            IKvp<Action<In1, In2>> consequence6,
            IKvp<Action<In1, In2>> consequence7,
            IKvp<Action<In1, In2>> consequence8,
            Action<string, In1, In2> fallback
        ) : this(
                new ManyOf<IKvp<Action<In1, In2>>>(
                    consequence1,
                    consequence2,
                    consequence3,
                    consequence4,
                    consequence5,
                    consequence6,
                    consequence7,
                    consequence8
                ),
                fallback
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(params IKvp<Action<In1, In2>>[] consequences) : this(
            new ManyOf<IKvp<Action<In1, In2>>>(consequences)
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IKvp<Action<In1, In2>>> consequences) : this(
            consequences,
            (unknown, input1, input2) => throw new ArgumentException($"Cannot find action for '{unknown}'")
        )
        { }

        /// <summary>
        /// An action fork that is dependant on a named condition.
        /// </summary>
        public ActionSwitch(IEnumerable<IKvp<Action<In1, In2>>> consequences, Action<string, In1, In2> fallback)
        {
            this.map = new MapOf<Action<In1, In2>>(consequences);
            this.fallback = fallback;
        }

        public void Invoke(string condition, In1 input1, In2 input2)
        {
            if (!this.map.ContainsKey(condition))
            {
                this.fallback(condition, input1, input2);
            }
            else
            {
                this.map[condition].Invoke(input1, input2);
            }
        }
    }
}
