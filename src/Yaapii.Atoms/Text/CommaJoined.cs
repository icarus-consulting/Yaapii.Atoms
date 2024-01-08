using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Texts joined together seperated by a comma followed by a blank.
    /// </summary>
    public sealed class CommaJoined : TextEnvelope
    {
        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(bool live, params String[] strs) :
            this(
                LiveMany.New(strs),
                live
            )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(IEnumerable<string> strs, bool live = false) :
            this(() => strs,
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(params IText[] txts) : this(false, txts)
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(params String[] strs) : this(false, strs)
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(bool live, params IText[] txts) : this(
            Mapped.New(
                txt => txt.AsString(),
                txts
            ),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(IEnumerable<IText> txts, bool live = false) : this(
            Mapped.New(
                txt => txt.AsString(),
                txts
            ),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(IScalar<IEnumerable<string>> txts, bool live = false) : this(
            () => txts.Value(),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a comma followed by a blank.
        /// </summary>
        public CommaJoined(Func<IEnumerable<string>> txts, bool live = false) : base(() =>
            string.Join(", ", txts()), live
        )
        { }
    }
}