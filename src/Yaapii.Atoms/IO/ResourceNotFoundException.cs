﻿// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Reflection;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Error
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
