﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Tests.IO
{
    /// <summary>
    /// A <see cref="StreamWriter"/> to a target.
    /// </summary>
    public sealed class WriterTo : StreamWriter, IDisposable
    {
        /// <summary>
        /// the target
        /// </summary>
        private readonly UncheckedScalar<StreamWriter> _target;

        /// <summary>
        /// A <see cref="StreamWriter"/> to a file <see cref="Uri"/>.
        /// </summary>
        /// <param name="path">a file Uri. Get with Path.GetFullPath(relOrAbsPath) or prefix with file:/// </param>
        public WriterTo(Uri path) : this(new OutputTo(path))
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> to a <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">the output stream</param>
        public WriterTo(Stream stream) : this(new OutputTo(stream))
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> to a <see cref="IOutput"/>.
        /// </summary>
        /// <param name="output">the output</param>
        public WriterTo(IOutput output) : this(output, Encoding.UTF8)
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> to a <see cref="IOutput"/>.
        /// </summary>
        /// <param name="output">the output</param>
        /// <param name="enc">encoding of the output</param>
        public WriterTo(IOutput output, Encoding enc) : this(
            () => new StreamWriter(output.Stream(), enc))
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> to a <see cref="IOutput"/>.
        /// </summary>
        /// <param name="output">the output</param>
        /// <param name="enc">encoding of the output</param>
        public WriterTo(IOutput output, string encoding) : this(
             () => new StreamWriter(output.Stream(), Encoding.GetEncoding(encoding)))
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> to a <see cref="IOutput"/> returned by a <see cref="Func{StreamWriter}"/>.
        /// </summary>
        /// <param name="output">the output</param>
        /// <param name="enc">encoding of the output</param>
        private WriterTo(Func<StreamWriter> fnc) : this(new ScalarOf<StreamWriter>(fnc))
        { }

        /// <summary>
        /// A <see cref="StreamWriter"/> to a <see cref="IOutput"/> encapsulated in a <see cref="IScalar{StreamWriter}"/>.
        /// </summary>
        /// <param name="tgt">the target streamwriter</param>
        private WriterTo(IScalar<StreamWriter> tgt) : base(new DeadStream())
        {
            this._target =
                new UncheckedScalar<StreamWriter>(
                    new StickyScalar<StreamWriter>(tgt));
        }

        public override void Write(char[] cbuf)
        {
            this._target.Value().Write(cbuf);
        }

        public override async Task FlushAsync()
        {
            await this._target.Value().FlushAsync();
        }

        public override void Write(bool value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(char value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            this._target.Value().Write(buffer, index, count);
        }

        public override void Write(decimal value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(double value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(int value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(long value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(object value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(string format, object arg0)
        {
            this._target.Value().Write(format, arg0);
        }

        public override void Write(string format, object arg0, object arg1)
        {
            this._target.Value().Write(format, arg0, arg1);
        }

        public override void Write(string format, object arg0, object arg1, object arg2)
        {
            this._target.Value().Write(format, arg0, arg1, arg2);
        }

        public override void Write(string format, params object[] arg)
        {
            this._target.Value().Write(format, arg);
        }

        public override void Write(string value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(uint value)
        {
            this._target.Value().Write(value);
        }

        public override void Write(ulong value)
        {
            this._target.Value().Write(value);
        }

        public override async Task WriteAsync(char value)
        {
            await this._target.Value().WriteAsync(value);
        }

        public override async Task WriteAsync(char[] buffer, int index, int count)
        {
            await this._target.Value().WriteAsync(buffer, index, count);
        }

        public override Task WriteAsync(string value)
        {
            return this._target.Value().WriteAsync(value);
        }

        public override void WriteLine()
        {
            this._target.Value().WriteLine();
        }

        public override void WriteLine(bool value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(char value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(char[] buffer)
        {
            this._target.Value().WriteLine(buffer);
        }

        public override void WriteLine(char[] buffer, int index, int count)
        {
            this._target.Value().WriteLine(buffer, index, count);
        }

        public override void WriteLine(decimal value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(double value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(float value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(int value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(long value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(object value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(string format, object arg0)
        {
            this._target.Value().WriteLine(format, arg0);
        }

        public override void WriteLine(string format, object arg0, object arg1)
        {
            this._target.Value().WriteLine(format, arg0, arg1);
        }

        public override void WriteLine(string format, object arg0, object arg1, object arg2)
        {
            this._target.Value().WriteLine(format, arg0, arg1, arg2);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            this._target.Value().WriteLine(format, arg);
        }

        public override void WriteLine(string value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(uint value)
        {
            this._target.Value().WriteLine(value);
        }

        public override void WriteLine(ulong value)
        {
            this._target.Value().WriteLine(value);
        }

        public override async Task WriteLineAsync()
        {
            await this._target.Value().WriteLineAsync();
        }

        public override async Task WriteLineAsync(char value)
        {
            await base.WriteLineAsync(value);
        }

        public override async Task WriteLineAsync(char[] buffer, int index, int count)
        {
            await this._target.Value().WriteLineAsync(buffer, index, count);
        }

        public override async Task WriteLineAsync(string value)
        {
            await this._target.Value().WriteLineAsync(value);
        }

        public override string NewLine { get => this._target.Value().NewLine; set => this._target.Value().NewLine = value; }

        public override void Write(float value)
        {
            this._target.Value().Write(value);
        }

        public override void Flush()
        {
            this._target.Value().Flush();
        }

        public new void Dispose()
        {
            try
            {
                this._target.Value().Flush();
            }
            catch (Exception) { }

            try
            {
                this._target.Value().BaseStream.Dispose();
                ((IDisposable)this._target.Value()).Dispose();
            }
            catch (Exception) { }
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum