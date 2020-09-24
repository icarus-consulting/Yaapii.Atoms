using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.IO
{
    public sealed class HeadOf : IInput
    {
        private readonly IInput origin;
        private readonly int length;

        public HeadOf(IInput origin, int length)
        {
            this.origin = origin;
            this.length = length;
        }

        public Stream Stream()
        {
            return new HeadInputStream(this.origin.Stream(), this.length);
        }
    }
}
