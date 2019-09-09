using System;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// The value of a particular bit.
    /// </summary>
    public sealed class BitAt : IScalar<bool>
    {
        private readonly IBytes bytes;
        private readonly int position;
        private readonly Func<IBytes, bool> fallback;

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
        {
            this.bytes = bytes;
            this.position = position;
            this.fallback = fallback;
        }

        public bool Value()
        {
            bool result;

            FailWhenIndexIsNegative();
            var byteIndex = this.position / 8;
            var bitInByteIndex = this.position % 8;
            var bytes = this.bytes.AsBytes();
            if (this.CanGetBit(bytes, byteIndex))
            {
                var relevantByte = bytes[byteIndex];
                result = (relevantByte & (1 << bitInByteIndex)) > 0;
            }
            else
            {
                result =
                    this.fallback.Invoke(
                        this.bytes
                    );
            }

            return result;
        }

        private void FailWhenIndexIsNegative()
        {
            if (this.position < 0)
            {
                throw new ArgumentException($"The position must be non-negative but is {this.position}");
            }
        }

        private bool CanGetBit(byte[] bytes, int byteIndex)
        {
            return bytes.Length > byteIndex;
        }
    }
}
