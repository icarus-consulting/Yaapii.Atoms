using System;
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
