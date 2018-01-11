using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Bytes
{
    /// <summary>
    /// Decodes all origin bytes using the Base64 encoding scheme.
    /// </summary>
    public sealed class Base64Bytes : IBytes
    {
        private readonly IBytes _bytes;

        /// <summary>
        /// Makes decoded origin bytes using the Base64 encoding scheme.
        /// </summary>
        /// <param name="bytes">origin bytes</param>
        public Base64Bytes(IBytes bytes)
        {
            _bytes = bytes;
        }

        /// <summary>
        /// The 
        /// </summary>
        /// <returns></returns>
        public byte[] AsBytes()
        {
            var bytes = _bytes.AsBytes();
            string base64String = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return Convert.FromBase64String(base64String);
        }
    }
}
