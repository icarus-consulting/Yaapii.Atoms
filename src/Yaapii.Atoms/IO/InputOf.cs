// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.IO
{
#pragma warning disable MaxClassLength // Class length max

    /// <summary>
    /// Input out of other things.
    /// </summary>
    public sealed class InputOf : IInput, IDisposable //@TODO IDisposable interface needs to be replaced with a better approach.
    {
        /// <summary>
        /// the input
        /// </summary>
        private readonly IScalar<Stream> _origin;

        /// <summary>
        /// Input out of a file Uri.
        /// </summary>
        /// <param name="file">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
        public InputOf(Uri file) : this(
            () =>
            {
                if (file.HostNameType == UriHostNameType.Dns)
                {
                    return WebRequest.Create(file).GetResponse().GetResponseStream();
                }
                else
                {
                    return new FileStream(Uri.UnescapeDataString(file.LocalPath), FileMode.Open, FileAccess.Read);
                }
            })
        { }

        /// <summary>
        /// Input out of a file Uri.
        /// </summary>
        /// <param name="file">uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
        public InputOf(FileInfo file) : this(new Live<FileInfo>(file))
        { }

        /// <summary>
        /// Input out of a scalar of a file Uri.
        /// </summary>
        /// <param name="file">scalar of a uri of a file, get with Path.GetFullPath(relativePath) or prefix with file://...</param>
        public InputOf(IScalar<FileInfo> file) : this(
            () => new FileStream(Uri.UnescapeDataString(file.Value().FullName), FileMode.Open, FileAccess.Read))
        { }

        /// <summary>
        /// Input out of a Url.
        /// </summary>
        /// <param name="url">a url starting with http:// or https://</param>
        public InputOf(Url url) : this(new Live<Url>(url))
        { }

        /// <summary>
        /// Input out of a Url scalar.
        /// </summary>
        /// <param name="url">a url starting with http:// or https://</param>
        public InputOf(IScalar<Url> url) : this(() =>
            {
                var stream = Task.Run(async () =>
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(url.Value().Value());
                        HttpContent content = response.Content;
                        {
                            return await content.ReadAsStreamAsync();
                        }
                    }
                });

                return stream.Result;
            })
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rdr">a stringreader</param>
        public InputOf(StringReader rdr) : this(new BytesOf(rdr))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        public InputOf(StreamReader rdr) : this(new BytesOf(rdr))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        /// <param name="enc">encoding of the reader</param>
        public InputOf(StreamReader rdr, Encoding enc) : this(new BytesOf(rdr, enc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="str">a stream</param>
        /// <param name="enc">encoding of the stream</param>
        public InputOf(Stream str, Encoding enc) : this(new BytesOf(new StreamReader(str), enc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="rdr">a streamreader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">maximum buffer size</param>
        public InputOf(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(new BytesOf(rdr, enc, max))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="builder">a stringbuilder</param>
        public InputOf(StringBuilder builder) : this(builder, Encoding.UTF8)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="builder">a stringbuilder</param>
        /// <param name="enc">encoding of the stringbuilder</param>
        public InputOf(StringBuilder builder, Encoding enc) : this(
            new Live<Stream>(
                () => new MemoryStream(
                    new BytesOf(builder, enc).AsBytes())))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="chars">some chars</param>
        public InputOf(params char[] chars) : this(new BytesOf(chars))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="chars">some chars</param>
        /// <param name="enc">encoding of the chars</param>
        public InputOf(char[] chars, Encoding enc) : this(new BytesOf(chars, enc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">some text</param>
        public InputOf(String text) : this(new BytesOf(text))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">some <see cref="string"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the string</param>
        public InputOf(String text, Encoding enc) : this(new BytesOf(text, enc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        public InputOf(IText text) : this(new BytesOf(text))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="text">some <see cref="IText"/></param>
        /// <param name="encoding"><see cref="Encoding"/> of the text</param>
        public InputOf(IText text, Encoding encoding) : this(new BytesOf(text, encoding))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="error"><see cref="Exception"/> to serialize</param>
        public InputOf(Exception error) : this(new BytesOf(error))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bytes">a <see cref="byte"/> array</param>
        public InputOf(byte[] bytes) : this(new BytesOf(bytes))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">a <see cref="IBytes"/> object which will be copied to memory</param>
        public InputOf(IBytes src) : this(new Live<Stream>(
                        () =>
                        {
                            var b = src.AsBytes();
                            var m = new MemoryStream();
                            m.Write(b, 0, b.Length);
                            m.Position = 0;
                            return m;
                        }))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="stream">a <see cref="Stream"/> as input</param>
        public InputOf(Stream stream) : this(new Live<Stream>(stream))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">a function retrieving a <see cref="Stream"/> as input</param>
        public InputOf(Func<Stream> fnc) : this(new Live<Stream>(fnc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="stream">the input <see cref="Stream"/></param>
        private InputOf(IScalar<Stream> stream)
        {
            this._origin = new ScalarOf<Stream>(stream, streamObj => !streamObj.CanRead);
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            return this._origin.Value();
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            Stream().Dispose();
        }
    }
}
