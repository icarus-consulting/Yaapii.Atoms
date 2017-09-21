using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// Input.
    ///
    /// <para>Here is for example how <see cref="IInput"/>  can be used
    /// in order to read the content of a text file:</para>
    ///
    /// <code>String content = new BytesAsText(
    /// new InputAsBytes(
    ///    new InputOf(new Uri("file:///C:/tmp/names.txt")
    ///   )
    /// ).asString();</code>
    ///
    /// <para>Here <see cref="Atoms.IO.InputOf"/> 
    /// implements <see cref="Atoms.IInput"/> and behaves like
    /// one, providing read-only access to the encapsulated <see cref="Uri"/> pointing to a file.</p>
    /// </summary>
    public interface IInput
    {
        Stream Stream();       
    }
}
