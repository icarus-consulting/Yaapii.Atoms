using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Bytes without data.
    /// </summary>
    public sealed class EmptyBytes : IBytes
    {
        /// <summary>
        /// Bytes without data.
        /// </summary>        
        public EmptyBytes()
        { }

        public byte[] AsBytes()
        {
            return new byte[] { };
        }
    }
}
