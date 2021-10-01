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

using System.Collections.Generic;

#pragma warning disable NoStatics // No Statics
#pragma warning disable VariablesArePrivate // Fields are private
#pragma warning disable CS1591

namespace Yaapii.Atoms.Misc
{
    /// <summary>
    /// <see cref="Comparer{T}"/> that can compare reverse to help reversing lists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IReverseCompare<T> : Comparer<T>
    {
        public static new readonly IReverseCompare<T> Default = new IReverseCompare<T>(Comparer<T>.Default);


        public static IReverseCompare<T> Reverse(Comparer<T> comparer)
        {
            return new IReverseCompare<T>(comparer);
        }

        private readonly Comparer<T> comparer = Default;

        private IReverseCompare(Comparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public override int Compare(T x, T y)
        {
            return comparer.Compare(y, x);
        }
    }
}
#pragma warning restore NoStatics // No Statics
#pragma warning restore VariablesArePrivate // Fields are private
