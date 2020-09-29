using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Input showing only last N bytes of the stream.
    /// </summary>
    public sealed class TailOf : IInput, IDisposable
    {

        private readonly IInput input;

        private readonly int count;

        private readonly int max;

        /// <summary>
        /// Input showing only last N bytes of the stream.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="bytes"></param>
        public TailOf(IInput input, int bytes) : this(input, bytes, 16384)
        {}

        /// <summary>
        /// Input showing only last N bytes of the stream.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="bytes"></param>
        /// <param name="max"></param>
        public TailOf(IInput input, int bytes, int max)
        {
            this.input = input;
            this.count = bytes;
            this.max = max;
        }

        public Stream Stream()
        {
            if(this.max < this.count)
            {
                throw new ArgumentException($"Can't tail {this.count} bytes if buffer is set to {this.max}");
            }
            var buffer = new byte[this.max];
            var response = new byte[this.count];
            int num = 0;
            var stream = this.input.Stream();

            for(int read = stream.Read(buffer, 0, buffer.Length); read > 0; read = stream.Read(buffer, 0, buffer.Length))
            {
                if(read < this.max && read < this.count)
                {
                    num = this.CopyPartial(buffer, response, num, read);
                }
                else
                {
                    num = this.Copy(buffer, response, read);
                }
            }
            return new MemoryStream(response, 0, num);

        }



        private int Copy(byte[] buffer, byte[] response, int read)
        {
            Array.Copy(buffer, read - this.count, response, 0, this.count);
            return new MinOf(this.count, read).AsInt();
        }


        private int CopyPartial(byte[] buffer, byte[] response, int num, int read)
        {
            int result;
            if (num > 0)
            {
                Array.Copy(response, read, response, 0, this.count - read);
                Array.Copy(buffer, 0, response, this.count - read, read);
                result = this.count;
            }
            else
            {
                Array.Copy(buffer, 0, response, 0, read);
                result = read;
            }
            return result;
        }

        public void Dispose()
        {
            try
            {
                (this.input as IDisposable)?.Dispose();
            }
            catch (Exception) { }
        }
    }
}
