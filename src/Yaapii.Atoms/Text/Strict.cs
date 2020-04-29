using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Text
{
    public sealed class Strict : TextEnvelope
    {
        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        public Strict(string candidate, params string[] valid) : this(
            candidate,
            new LiveMany<string>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        public Strict(string candidate, IEnumerable<string> valid) : this(
            candidate,
            true,
            valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        public Strict(string candidate, bool ignoreCase, params string[] valid) : this(
            candidate, ignoreCase, new LiveMany<string>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        public Strict(string candidate, bool ignoreCase, IEnumerable<string> valid) : this(
            new LiveText(candidate), ignoreCase, new Mapped<string, IText>((str) => new LiveText(str), valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        public Strict(IText candidate, params string[] valid) : this(
            candidate, true, valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        public Strict(IText candidate, bool ignoreCase, params string[] valid) : this(
            candidate,
            ignoreCase,
            new Mapped<string, IText>((str) => new LiveText(str), valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        public Strict(IText candidate, params IText[] valid) : this(candidate, true, valid)
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        public Strict(IText candidate, bool ignoreCase, params IText[] valid) : this(
            candidate,
            ignoreCase,
            new LiveMany<IText>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        public Strict(IText candidate, IEnumerable<IText> valid) : this(
            candidate,
            true,
            valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        public Strict(IText candidate, bool ignoreCase, IEnumerable<IText> allowed) : this(
            candidate,
            allowed,
            () => ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)
        { }

        private Strict(IText candidate, IEnumerable<IText> allowed, Func<StringComparison> ignoreCase) : base(() =>
        {
            var valid = false;
            var str = candidate.AsString();
            foreach (var txt in allowed)
            {
                if (txt.AsString().Equals(str, ignoreCase()))
                {
                    valid = true;
                    break;
                }
            }
            if (!valid)
            {
                throw new ArgumentException($"'{str}' is not valid here - expected: {new Joined(", ", allowed).AsString()}");
            }
            return str;
        }, false)
        { }
    }
}
