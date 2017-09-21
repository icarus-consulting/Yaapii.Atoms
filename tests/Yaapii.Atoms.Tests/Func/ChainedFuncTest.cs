using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    class ChainedFuncTest
    {
        [Fact]
        public void WithoutIterable()
        {
            Assert.True(
            new LengthOf<string>(
                new Filtered<string>(
                    new Mapped<string, string>(
                        new EnumerableOf<string>("public", "final", "class"),
                        new ChainedFunc<String, String, String>(
                            input => input += "X",
                            input => input += "Y"
                        )
                    ),
                    input => input.EndsWith("XY")
                )
            ).Value() == 3,
            "cannot chain functions");
        }

        [Fact]
        public void WithIterable()
        {
            Assert.True(
            new LengthOf<string>(
                new Filtered<string>(
                    new Mapped<string, string>(
                        new EnumerableOf<string>("private", "static", "String"),
                        new ChainedFunc<string, string, string>(
                            input => input += "1",
                            new EnumerableOf<IFunc<string, string>>(
                                new FuncOf<string, string>(input => input += ("2")),
                                new FuncOf<string, string>(input => input.Replace("a", "b"))
                            ),
                            input => input.Trim()
                        )
                    ),
                    input => !input.StartsWith("st") && input.EndsWith("12")
                    )
                 ).Value() == 2);
        }
    }
}
