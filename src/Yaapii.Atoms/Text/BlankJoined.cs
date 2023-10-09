using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Texts joined together seperated by a blank.
    /// </summary>
    public sealed class BlankJoined : TextEnvelope
    {
        /// <summary>
        /// Texts joined together seperated by a blank.
        /// </summary>
        public BlankJoined(bool live, params String[] strs) :
            this(
                LiveMany.New(strs),
                live
            )
        { }

        /// <summary>
        /// Texts joined together seperated by a blank.
        /// </summary>
        public BlankJoined(IEnumerable<string> strs, bool live = false) :
            this(() => strs,
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a blank.
        /// </summary>
        public BlankJoined(params IText[] txts) : this(false, txts)
        { }

        /// <summary>
        /// Texts joined together seperated by a blank.
        /// </summary>
        public BlankJoined(params String[] strs) : this(false, strs)
        { }

        /// <summary>
        /// Texts joined together seperated by a blank.
        /// </summary>
        public BlankJoined(bool live, params IText[] txts) : this(
            Mapped.New(
                txt => txt.AsString(),
                txts
            ),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a blank.
        /// </summary>
        public BlankJoined(IEnumerable<IText> txts, bool live = false) : this(
            Mapped.New(
                txt => txt.AsString(),
                txts
            ),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a blank.
        /// </summary>
        public BlankJoined(IScalar<IEnumerable<string>> txts, bool live = false) : this(
            () => txts.Value(),
            live
        )
        { }

        /// <summary>
        /// Texts joined together seperated by a blank.
        /// </summary>
        public BlankJoined(Func<IEnumerable<string>> txts, bool live = false) : base(() =>
            string.Join(" ", txts()), live
        )
        { }
    }
}