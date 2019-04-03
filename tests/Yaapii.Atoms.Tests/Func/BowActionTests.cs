using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class BowActionTests
    {
        [Fact]
        public void WaitsForTrigger()
        {
            var actions = new List<string>();
            var count = 0;
            new BowAction(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("shoot")
            ).Invoke();

            Assert.Equal(
                "ask trigger, ask trigger, shoot",
                new JoinedText(", ", actions).AsString()
            );
        }

        [Fact]
        public void Prepares()
        {
            var actions = new List<string>();
            var count = 0;
            new BowAction(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("prepare"),
                () => actions.Add("shoot")
            ).Invoke();

            Assert.Equal(
                "prepare, ask trigger, ask trigger, shoot",
                new JoinedText(", ", actions).AsString()
            );
        }

        [Fact]
        public void CancelsAfterTimeout()
        {
            Assert.Throws<ApplicationException>(
                () =>
                    new BowAction(
                        () => false,
                        () => { },
                        new TimeSpan(0, 0, 0, 0, 100)
                    ).Invoke()
            );
        }
    }
}
