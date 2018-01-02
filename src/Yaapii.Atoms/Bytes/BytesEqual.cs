using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaapii.Atoms.Bytes
{
    /// <summary>
    /// Equality for <see cref="IBytes"/>
    /// </summary>
    public sealed class BytesEqual : IScalar<bool>
    {
        private readonly IBytes _left;
        private readonly IBytes _right;

        /// <summary>
        /// Makes a truth about <see cref="IBytes"/> are equal or not.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public BytesEqual(IBytes left, IBytes right)
        {
            _left = left;
            _right = right;
        }

        /// <summary>
        /// Equal or not
        /// </summary>
        /// <returns></returns>
        public bool Value()
        {
            var left = _left.AsBytes();
            var right = _right.AsBytes();
            var equal = left.Length == right.Length;

            for(var i=0;i<left.Length && equal;i++)
            {
                if(left[i] != right[i])
                {
                    equal = false;
                    break;
                }
            }

            return equal;
        }
    }
}
