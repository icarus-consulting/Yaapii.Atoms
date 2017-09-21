using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Input with no data.
    /// </summary>
    public sealed class DeadInput : IInput
    {
        /// <summary>
        /// Input with no data.
        /// </summary>
        public DeadInput()
        { }

        public Stream Stream()
        {
            return new BytesAsInput(new EmptyBytes()).Stream();
        }
    }
}
