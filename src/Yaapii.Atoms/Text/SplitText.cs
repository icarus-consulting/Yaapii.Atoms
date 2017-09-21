using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    public sealed class SplitText : IEnumerable<String>
    {
        private readonly UncheckedText _origin;
        private readonly UncheckedText _regex;

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        public SplitText(String text, String rgx) :
            this(
                new UncheckedText(new TextOf(text)),
                new UncheckedText(new TextOf(rgx))
            )
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>        
        public SplitText(String text, IText rgx) :
            this(new UncheckedText(text), new UncheckedText(rgx))
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        public SplitText(IText text, String rgx) :
            this(new UncheckedText(text), new UncheckedText(rgx))
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        public SplitText(IText text, IText rgx) :
            this(new UncheckedText(text), new UncheckedText(rgx))
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        public SplitText(UncheckedText text, UncheckedText rgx)
        {
            this._origin = text;
            this._regex = rgx;
        }

        public IEnumerator<String> GetEnumerator()
        {
            return new Filtered<String>(
                    new EnumerableOf<String>(
                        new Regex(this._regex.AsString()).Split(this._origin.AsString())
                    ),
                    (str) => !String.IsNullOrEmpty(str))
                    .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics