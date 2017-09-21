using System;
using System.IO;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> which doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    public sealed class UncheckedText : IText
    {
        private readonly Atoms.IText _text;
        private readonly IFunc<IOException, String> _fallback;

        /// <summary>
        /// A <see cref="IText"/> which doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="text">text to uncheck</param>
        public UncheckedText(String text) : this(new TextOf(text))
        { }

        /// <summary>
        /// A <see cref="IText"/> which doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="text">text to uncheck</param>
        public UncheckedText(IText text) : this(
                text,
                new FuncOf<IOException, string>(
                    (error) =>
                    {
                        throw new UncheckedIOException(error);
                    }))
        { }

        /// <summary>
        /// A <see cref="IText"/> which doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="txt">text to uncheck</param>
        /// <param name="fbk">fallback function</param>
        public UncheckedText(IText txt, IFunc<IOException, String> fbk)
        {
            _text = txt;
            _fallback = fbk;
        }

        public String AsString()
        {
            String txt;
            try
            {
                txt = _text.AsString();
            }
            catch (IOException ex)
            {
                txt = new UncheckedFunc<IOException, String>(_fallback).Invoke(ex);
            }
            return txt;
        }

        public int CompareTo(Atoms.IText text)
        {
            return this.AsString().CompareTo(
                new UncheckedText(text).AsString()
            );
        }

        public bool Equals(Atoms.IText other)
        {
            return other.AsString().Equals(this.AsString());
        }
    }
}
