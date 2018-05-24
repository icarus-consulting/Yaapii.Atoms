﻿
// MIT License
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
// SOFTWARE


.using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Text
{
	public sealed class ComparableText : IText, IComparable

	{
		private readonly IText text;

		public ComparableText(IText text)
		{
			this.text = text;
		}

		public string AsString()
		{
			return this.text.AsString();
		}

		public bool Equals(IText other)
		{
			return this.text.Equals(other);
		}

		public int CompareTo(object obj)
		{
			new FailNull(
				obj as IText,
				"Object can´t be casted as Itext"
			).Go();
			return this.text.AsString().CompareTo(((IText)obj).AsString());
		}
	}
}