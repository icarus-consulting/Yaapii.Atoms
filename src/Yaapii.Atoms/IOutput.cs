using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms
{
    /// Output.
    ///
    /// <para>Here is for example how <see cref="IOutput""/> can be used
    /// together with <see cref="IInput"/> in order to modify the content
    /// of a text file:</p>
    ///
    /// <code> new LengthOfInput(
    ///   new TeeInput(
    ///     new InputOf(new StringAsText("Hello, world!")),
    ///     new OutputTo(new Uri("file:///C:/tmp/names.txt"))
    ///   )
    /// ).AsValue();</code>
    ///
    /// <para>Here <see cref="Atoms.IO.OutputTo"/> implements {@link Output} and behaves like
    /// one, providing write-only access to the encapsulated
    /// <see cref="Uri"/>. The <see cref="TeeInput"/> copies the content of the
    /// input to the output. The <see cref="LengthOf"/>
    /// calculates the size of the copied data.</para>
    ///
    /// <para>There is no thread-safety guarantee.</para>
    ///
    public interface IOutput
    {
        Stream Stream();
    }
}