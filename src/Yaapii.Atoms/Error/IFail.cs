using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Something that can fail when calling go.
    /// </summary>
    public interface IFail
    {
        void Go();
    }
}
