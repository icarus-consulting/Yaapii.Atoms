using System;
using System.Collections.Generic;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> of texts joined together.
    /// </summary>
    public sealed class JoinedText : IText
    {
        private readonly IScalar<IEnumerable<IText>> _texts;
        private readonly IText _delimiter;

        /// <summary>
        /// Joins A <see cref="IText"/>s together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public JoinedText(String delimit, params String[] strs) :
            this(
                delimit,
                new EnumerableOf<string>(strs)
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public JoinedText(String delimit, IEnumerable<String> strs) :
            this(
                new TextOf(delimit),
                new Mapped<string, IText>(
                    strs,
                    new FuncOf<string, IText>((text) => new TextOf(text))
                )
            )
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public JoinedText(IText delimit, params IText[] txts) : this(delimit, new EnumerableOf<IText>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public JoinedText(String delimit, params IText[] txts) : this(new TextOf(delimit), txts)
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public JoinedText(String delimit, IEnumerable<IText> txts) : this(new TextOf(delimit), new ScalarOf<IEnumerable<IText>>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public JoinedText(String delimit, System.Func<IEnumerable<IText>> txts) : this(new TextOf(delimit), new ScalarOf<IEnumerable<IText>>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public JoinedText(IText delimit, System.Func<IEnumerable<IText>> txts) : this(delimit, new ScalarOf<IEnumerable<IText>>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">texts to join</param>
        public JoinedText(IText delimit, IEnumerable<IText> txts) : this(delimit, new ScalarOf<IEnumerable<IText>>(txts))
        { }

        /// <summary>
        /// Joins texts together with the delimiter between them.
        /// </summary>
        /// <param name="delimit">delimiter</param>
        /// <param name="strs">scalars of texts to join</param>
        private JoinedText(IText delimit, IScalar<IEnumerable<IText>> txts)
        {
            this._delimiter = delimit;
            this._texts = txts;
        }

        public String AsString()
        {
            return String.Join(
                this._delimiter.AsString(), 
                new Mapped<IText, string>(
                    this._texts.Value(), 
                    text => text.AsString()));
        }

        public int CompareTo(IText text)
        {
            return new UncheckedText(this).CompareTo(text);
        }

        public bool Equals(IText other)
        {
            return CompareTo(other) == 0;
        }
    }
}
