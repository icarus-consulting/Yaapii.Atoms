using System;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// The value of a particular bit.
    /// </summary>
    public sealed class BitAt : ScalarEnvelope<bool>
    {
        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="exception">Exception which is thrown in case of an error</param>
        public BitAt(IBytes bytes, Exception exception) : this(
            bytes,
            0,
            exception
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="exception">Exception which is thrown in case of an error</param>
        public BitAt(IBytes bytes, int position, Exception exception) : this(
            bytes,
            position,
            itr => throw exception
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        public BitAt(IBytes bytes) : this(
            bytes,
            itr => throw new ArgumentException($"Cannot get first bit because there are only {bytes.AsBytes().Length} bytes.")
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, bool fallback) : this(
            bytes,
            itr => fallback
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, bool fallback) : this(
            bytes,
            itr => fallback
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, Func<IBytes, bool> fallback) : this(
            bytes,
            0,
            fallback
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, IFunc<IBytes, bool> fallback) : this(
            bytes,
            0,
            fallback
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        public BitAt(IBytes bytes, int position) : this(
            bytes,
            position,
            itr => throw new ArgumentException($"Cannot get bit at position {position} because there are only {bytes.AsBytes().Length} bytes.")
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, IFunc<IBytes, bool> fallback) : this(
            bytes,
            position,
            (bts) => fallback.Invoke(bts)
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, Func<IBytes, bool> fallback)
            : base(() =>
            {
                if (position < 0)
                {
                    throw new ArgumentException($"The position must be non-negative but is {position}");
                }

                bool result;
                var byteIndex = position / 8;
                var bitInByteIndex = position % 8;
                var bytesArr = bytes.AsBytes();
                if (bytesArr.Length > byteIndex)
                {
                    var relevantByte = bytesArr[byteIndex];
                    result = (relevantByte & (1 << bitInByteIndex)) > 0;
                }
                else
                {
                    result = fallback.Invoke(bytes);
                }

                return result;
            })
        { }
    }
}
