using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Yaapii.Atoms.Text;

namespace Yaapii.IO.Error
{
    /// <summary>
    /// When a resource cannot be found.
    /// </summary>
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        /// Tell which one was searched for and which are available.
        /// </summary>
        /// <param name="missing">the missing one</param>
        /// <param name="container">the searched container</param>
        public ResourceNotFoundException(string missing, Assembly container) : base(
            new FormattedText(
                "Resource '{0}' not found.\r\n{1} resources are available\r\n{2}",
                missing,
                container.GetManifestResourceNames().Length,
                new JoinedText("\r\n", container.GetManifestResourceNames()).AsString()
            ).AsString()
        )
        { }
    }
}
