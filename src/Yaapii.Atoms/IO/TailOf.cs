using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// <see cref="IInput"/> which will be read from EOF 
    /// </summary>
    public sealed class TailOf : IInput, IDisposable
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IInput input;

        private readonly int count;

        private readonly int max;


        public TailOf(IInput input, int bytes) : this(input, bytes, 16384)
        {}

        public TailOf(IInput input, int bytes, int max)
        {
            this.input = input;
            this.count = bytes;
            this.max = max;
        }

        /// <summary>
        /// Retrieves the stream for reading the tail
        /// </summary>
        /// <returns></returns>
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


        /**
     * Copy full buffer to response.
     * @param buffer The buffer array
     * @param response The response array
     * @param read Number of bytes read in buffer
     * @return Number of bytes in the response buffer
     */
        private int Copy(byte[] buffer, byte[] response, int read)
        {
            Array.Copy(buffer, read - this.count, response, 0, this.count);
            return new MinOf(this.count, read).AsInt();
        }

        /**
         * Copy buffer to response for read count smaller then buffer size.
         * @param buffer The buffer array
         * @param response The response array
         * @param num Number of bytes in response array from previous read
         * @param read Number of bytes read in the buffer
         * @return New count of bytes in the response array
         * @checkstyle ParameterNumberCheck (3 lines)
         */
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


        /// <summary>
        /// Clean up
        /// </summary>
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
