using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// Checks if a text is blank.
    /// </summary>
    public sealed class IsBlank : IScalar<Boolean>
    {
        private readonly IText _origin;

        /// <summary>
        /// Checks if a A <see cref="IText"/> is blank.
        /// </summary>
        /// <param name="text">text to check</param>
        public IsBlank(IText text)
        {
            this._origin = text;
        }

        public Boolean Value()
        {
            return !this._origin.AsString().ToCharArray().Any(c => !String.IsNullOrWhiteSpace(c + ""));
        }
    }
}
