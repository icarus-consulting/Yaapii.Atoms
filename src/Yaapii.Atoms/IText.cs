using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    ///
    /// Text.
    ///
    /// <para>There is no thread-safety guarantee.</para>
    ///
    public interface IText : IEquatable<IText>
    {
        String AsString();

    }
}
