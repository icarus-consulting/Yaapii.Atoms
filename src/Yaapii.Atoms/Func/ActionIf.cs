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
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Conditional Action as part of an <see cref="ActionSwitch{In}"/>
    /// </summary>
    public sealed class ActionIf<In> : KvpEnvelope<Action<In>>
    {
        /// <summary>
        /// Conditional Action as part of an <see cref="ActionSwitch{In}"/>
        /// </summary>
        public ActionIf(string condition, Action<In> consequence) : base(
            new KvpOf<Action<In>>(condition, consequence)
        )
        { }
    }

    /// <summary>
    /// Conditional Action as part of an <see cref="ActionSwitch{In1, In2}"/>
    /// </summary>
    public sealed class ActionIf<In1, In2> : KvpEnvelope<Action<In1, In2>>
    {
        /// <summary>
        /// Conditional Action as part of an <see cref="ActionSwitch{In1, In2}"/>
        /// </summary>
        public ActionIf(string condition, Action<In1, In2> consequence) : base(
            new KvpOf<Action<In1, In2>>(condition, consequence)
        )
        { }
    }

    public static class ActionIf
    {
        /// <summary>
        /// ctor
        /// </summary>
        public static IKvp<Action<T>> New<T>(string condition, Action<T> consequence) => new ActionIf<T>(condition, consequence);

        /// <summary>
        /// ctor
        /// </summary>
        public static IKvp<Action<In1, In2>> New<In1, In2>(string condition, Action<In1, In2> consequence) => new ActionIf<In1, In2>(condition, consequence);
    }
}
