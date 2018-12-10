using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Bytes
{
    /// <summary>
    /// Encodes all origin bytes using the Base64 encoding scheme.
    /// </summary>
    public sealed class BytesBase64 : IBytes
    {
        private readonly IBytes bytes;

        /// <summary>
        /// Encoded origin bytes using the Base64 encoding scheme.
        /// </summary>
        /// <param name="bytes"></param>
        public BytesBase64(IBytes bytes)
        {
            this.bytes = bytes;
        }

        /// <summary>
        /// The bytes encoded as Base64
        /// </summary>
        /// <returns></returns>
        public byte[] AsBytes()
        {
            return 
                Encoding.UTF8.GetBytes(
                    Convert.ToBase64String(
                        bytes.AsBytes()
                    )
                );
        }
    }
}
