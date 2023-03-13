// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Text
{
    /// <summary>
    /// A paragraph which seperates the given lines by a carriage return.
    /// </summary>
    public sealed class Paragraph : TextEnvelope
    {
        #region string head, IEnumerable<string> lines, params string[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => new LiveText(line),
                new Joined<string>(
                    new LiveMany<string>(head),
                    lines,
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => new LiveText(line),
                new Joined<string>(
                    new LiveMany<string>(head1, head2),
                    lines,
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => new LiveText(line),
                new Joined<string>(
                    new LiveMany<string>(head1, head2, head3),
                    lines,
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => new LiveText(line),
                new Joined<string>(
                    new LiveMany<string>(head1, head2, head3, head4),
                    lines,
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => new LiveText(line),
                new Joined<string>(
                    new LiveMany<string>(head1, head2, head3, head4, head5),
                    lines,
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => new LiveText(line),
                new Joined<string>(
                    new LiveMany<string>(head1, head2, head3, head4, head5, head6),
                    lines,
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, string head7, IEnumerable<string> lines, params string[] tail) : this(
            new Mapped<string, IText>(
                line => new LiveText(line),
                new Joined<string>(
                    new LiveMany<string>(head1, head2, head3, head4, head5, head6, head7),
                    lines,
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        #endregion

        #region IText head1, IEnumerable<string> lines, params string[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        lines,
                        new LiveMany<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        lines,
                        new LiveMany<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        lines,
                        new LiveMany<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        lines,
                        new LiveMany<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        lines,
                        new LiveMany<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5, head6),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        lines,
                        new LiveMany<string>(tail)
                    )
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IText head7, IEnumerable<string> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5, head6, head7),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        lines,
                        new LiveMany<string>(tail)
                    )
                )
            )
        )
        { }
        #endregion

        #region string head, IEnumerable<IText> lines, params string[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head)
                ),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2)
                ),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3)
                ),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3, head4)
                ),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3, head4, head5)
                ),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3, head4, head5, head6)
                ),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, string head7, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3, head4, head5, head6, head7)
                ),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        #endregion

        #region IText head, IEnumerable<IText> lines, params string[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5, head6),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IText head7, IEnumerable<IText> lines, params string[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5, head6, head7),
                lines,
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(tail)
                )
            )
        )
        { }
        #endregion

        #region string head, IEnumerable<string> lines, params IText[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        new LiveMany<string>(head),
                        lines
                    )
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        new LiveMany<string>(head1, head2),
                        lines
                    )
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        new LiveMany<string>(head1, head2, head3),
                        lines
                    )
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        new LiveMany<string>(head1, head2, head3, head4),
                        lines
                    )
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        new LiveMany<string>(head1, head2, head3, head4, head5),
                        lines
                    )
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        new LiveMany<string>(head1, head2, head3, head4, head5, head6),
                        lines
                    )
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, string head7, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new Joined<string>(
                        new LiveMany<string>(head1, head2, head3, head4, head5, head6, head7),
                        lines
                    )
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        #endregion

        #region IText head, IEnumerable<string> lines, params IText[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    lines
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    lines
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    lines
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    lines
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    lines
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5, head6),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    lines
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IText head7, IEnumerable<string> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5, head6, head7),
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    lines
                ),
                new LiveMany<IText>(tail)
            )
        )
        { }
        #endregion

        #region string head, IEnumerable<IText> lines, params IText[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head)
                ),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2)
                ),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3)
                ),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3, head4)
                ),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3, head4, head5)
                ),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3, head4, head5, head6)
                ),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(string head1, string head2, string head3, string head4, string head5, string head6, string head7, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new Mapped<string, IText>(
                    line => new LiveText(line),
                    new LiveMany<string>(head1, head2, head3, head4, head5, head6, head7)
                ),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        #endregion

        #region IText head, IEnumerable<IText> lines, params IText[] tail
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5, head6),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IText head1, IText head2, IText head3, IText head4, IText head5, IText head6, IText head7, IEnumerable<IText> lines, params IText[] tail) : this(
            new Joined<IText>(
                new LiveMany<IText>(head1, head2, head3, head4, head5, head6, head7),
                lines,
                new LiveMany<IText>(tail)
            )
        )
        { }
        #endregion


        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(params string[] lines) : this(new Mapped<string, IText>(line => new LiveText(line), lines))
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IEnumerable<string> lines) : this(new Mapped<string, IText>(line => new TextOf(line), lines))
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(params IText[] lines) : this(new LiveMany<IText>(lines))
        { }
        /// <summary>
        /// A paragraph which seperates the given lines by a carriage return.
        /// </summary>
        public Paragraph(IEnumerable<IText> lines)
            : base(new Joined(Environment.NewLine, lines), false)
        { }
    }
}
