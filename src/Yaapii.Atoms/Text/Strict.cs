using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A strict text which can only be one of the specified valid texts.
    /// </summary>
    public sealed class Strict : TextEnvelope
    {
        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(string candidate, params string[] valid) : this(
            candidate,
            new LiveMany<string>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(string candidate, IEnumerable<string> valid) : this(
            candidate,
            true,
            valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(string candidate, bool ignoreCase, params string[] valid) : this(
            candidate, ignoreCase, new ManyOf<string>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(string candidate, bool ignoreCase, IEnumerable<string> valid) : this(
            new LiveText(candidate), ignoreCase, new Mapped<string, IText>((str) => new LiveText(str), valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, params string[] valid) : this(
            candidate, true, valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, bool ignoreCase, params string[] valid) : this(
            candidate,
            ignoreCase,
            new Mapped<string, IText>((str) => new LiveText(str), valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, params IText[] valid) : this(candidate, true, valid)
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, bool ignoreCase, params IText[] valid) : this(
            candidate,
            ignoreCase,
            new LiveMany<IText>(valid)
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts, ignoring case.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, IEnumerable<IText> valid) : this(
            candidate,
            true,
            valid
        )
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="ignoreCase">Ignore case in the canidate and valid texts</param>
        /// <param name="valid">The valid texts</param>
        public Strict(IText candidate, bool ignoreCase, IEnumerable<IText> valid) : this(
            candidate,
            valid,
            new ScalarOf<StringComparison>(() => ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
        { }

        /// <summary>
        /// A strict text which can only be one of the specified valid texts.
        /// </summary>
        /// <param name="candidate">The canidate to check for valid texts</param>
        /// <param name="valid">The valid texts</param>
        /// <param name="stringComparer">Ignore case in the canidate and valid texts</param>
        private Strict(IText candidate, IEnumerable<IText> valid, IScalar<StringComparison> stringComparer) : base(() =>
        {
            var result = false;
            var str = candidate.AsString();
            foreach (var txt in valid)
            {
                if (txt.AsString().Equals(str, stringComparer.Value()))
                {
                    result = true;
                    break;
                }
            }
            if (!result)
            {
                throw new ArgumentException($"'{str}' is not valid here - expected: {new Joined(", ", valid).AsString()}");
            }
            return str;
        }, false)
        { }
    }
}
