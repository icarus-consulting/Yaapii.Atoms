using System;
using Yaapii.Atoms.Error;
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
        private readonly IBiFunc<Exception, IBytes, bool> fallback;

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
            new FuncOf<IBytes, bool>(itr =>
                throw exception
            )
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        public BitAt(IBytes bytes) : this(
            bytes,
            new BiFuncOf<Exception, IBytes, bool>((exception, itr) =>
                throw new ArgumentException($"Cannot get first bit: {exception.Message}")
            )
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, bool fallback) : this(
            bytes,
            new FuncOf<IBytes, bool>(itr =>
                fallback
            )
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
            new FuncOf<IBytes, bool>(itr =>
                fallback
            )
        )
        { }

        /// <summary>
        /// The value of the first bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, IBiFunc<Exception, IBytes, bool> fallback) : this(
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
        public BitAt(IBytes bytes, Func<IBytes, bool> fallback) : this(
            bytes,
            0,
            new FuncOf<IBytes, bool>(fallback)
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
            new BiFuncOf<Exception, IBytes, bool>((exception, itr) =>
            {
                throw new ArgumentException($"Cannot get bit at position {position}: {exception.Message}");
            })
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
            (exception, bts) => fallback.Invoke(bts)
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, Func<IBytes, bool> fallback) : this(
            bytes,
            position,
            new BiFuncOf<Exception, IBytes, bool>((exception, bts) => fallback.Invoke(bts))
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, Func<Exception, IBytes, bool> fallback) : this(
            bytes,
            position,
            new BiFuncOf<Exception, IBytes, bool>((exception, bts) => fallback.Invoke(exception, bts))
        )
        { }

        /// <summary>
        /// The value of a particular bit.
        /// </summary>
        /// <param name="bytes">Bytes from where the bit is taken</param>
        /// <param name="position">Zero based bit index in the bytes</param>
        /// <param name="fallback">Result in case of an error</param>
        public BitAt(IBytes bytes, int position, IBiFunc<Exception, IBytes, bool> fallback)
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
            if (this.CanGetBit(byteIndex))
            {
                var relevantByte = this.bytes.AsBytes()[byteIndex];
                result = (relevantByte & (1 << bitInByteIndex)) > 0;
            }
            else
            {
                result =
                    this.fallback.Invoke(
                        new ArgumentException($"Cannot get byte at position {byteIndex} because there are only {this.bytes.AsBytes().Length} bytes."),
                        this.bytes
                    );
            }

            return result;
        }

        private void FailWhenIndexIsNegative()
        {
            new FailWhen(() =>
                this.position < 0,
                new ArgumentException($"The position must be non-negative but is {this.position}")
            ).Go();
        }

        private bool CanGetBit(int byteIndex)
        {
            return this.bytes.AsBytes().Length > byteIndex;
        }
    }
}
