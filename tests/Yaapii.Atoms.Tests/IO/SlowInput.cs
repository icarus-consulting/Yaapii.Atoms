// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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

using System.IO;

namespace Yaapii.Atoms.IO.Tests
{
    internal sealed class SlowInput : IInput
    {

        /**
         * Original input.
         */
        private readonly IInput _origin;

        /**
         * Ctor.
         * @param size The size of the array to encapsulate
         */
        internal SlowInput(long size) : this((int)size)
        { }

        /**
         * Ctor.
         * @param size The size of the array to encapsulate
         */
        internal SlowInput(int size) : this(new InputOf(new MemoryStream(new byte[size])))
        { }

        /**
         * Ctor.
         * @param input Original input to encapsulate and make slower
         */
        internal SlowInput(IInput input)
        {
            this._origin = input;
        }

        public Stream Stream()
        {
            return new SlowInputStream(this._origin.Stream());
        }

    }
}
