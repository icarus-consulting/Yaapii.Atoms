using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    ///
    /// Text.
    ///
    public interface IText : IEquatable<IText>
    {
        String AsString();
    }
}
