using System.Text.RegularExpressions;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Checks if a text starts with an given content.
    /// </summary>
    public sealed class StartsWith : IScalar<bool>
    {
        private readonly IText _text;
        private readonly IText _start;

        /// <summary>
        /// Checks if a <see cref="IText"/> ends with an given <see cref="string"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="start">Starting content to use in the test</param>
        public StartsWith(IText text, string start) : this(
            text,
            new TextOf(start)
        )
        { }

        /// <summary>
        /// Checks if a <see cref="IText"/> ends with an given <see cref="IText"/>
        /// </summary>
        /// <param name="text">Text to test</param>
        /// <param name="start">Starting content to use in the test</param>
        public StartsWith(IText text, IText start)
        {
            this._text = text;
            this._start = start;
        }

        /// <summary>
        /// Gets the result
        /// </summary>
        /// <returns>The result</returns>
        public bool Value()
        {
            var regex = new Regex("^" + Regex.Escape(this._start.AsString()));
            return regex.IsMatch(this._text.AsString());
        }
    }
}
