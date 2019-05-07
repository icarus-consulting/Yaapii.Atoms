﻿// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Enumerable;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591
namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    public sealed class Split : IEnumerable<String>
    {
        private readonly IText _origin;
        private readonly IText _regex;
        private readonly bool _remBlank;

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
        public Split(String text, String rgx, bool remBlank = true) : this(
            new TextOf(text),
            new TextOf(rgx),
            remBlank)
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
        public Split(String text, IText rgx, bool remBlank = true) : this(
            new TextOf(text),
            rgx,
            remBlank)
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
        public Split(IText text, String rgx, bool remBlank = true) : this(
            text,
            new TextOf(rgx),
            remBlank)
        { }

        /// <summary>
        /// A <see cref="IText"/> which has been splitted at the given string.
        /// </summary>
        /// <param name="text">text to split</param>
        /// <param name="rgx">regex to use for splitting</param>
        /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
        public Split(IText text, IText rgx, bool remBlank = true)
        {
            this._origin = text;
            this._regex = rgx;
            this._remBlank = remBlank;
        }

        public IEnumerator<String> GetEnumerator()
        {
            var splitted =
                new EnumerableOf<String>(
                    new Regex(this._regex.AsString()).Split(this._origin.AsString()));

            return 
                this._remBlank 
                ? new Filtered<String>(
                    //(str) => !String.IsNullOrEmpty(str),
                    (str) => !String.IsNullOrWhiteSpace(str),
                    splitted).GetEnumerator() 
                : splitted.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics