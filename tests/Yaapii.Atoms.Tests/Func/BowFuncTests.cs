using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class BowFuncTests
    {
        [Fact]
        public void WaitsForTrigger()
        {
            var actions = new List<string>();
            var count = 0;
            new BowFunc<string>(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                (str) => actions.Add("shoot")
            ).Invoke("test");

            Assert.Equal(
                "ask trigger, ask trigger, shoot",
                new Joined(", ", actions).AsString()
            );
        }

        [Fact]
        public void Prepares()
        {
            var actions = new List<string>();
            var count = 0;
            new BowFunc<string>(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("prepare"),
                (str) => actions.Add("shoot"),
                new TimeSpan(0,0,10)
            ).Invoke("test");

            Assert.Equal(
                "prepare, ask trigger, ask trigger, shoot",
                new Joined(", ", actions).AsString()
            );
        }

        [Fact]
        public void InvokesShoot()
        {
            var actions = new List<string>();
            var count = 0;
            new BowFunc<string>(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("prepare"),
                (str) => actions.Add(str),
                new TimeSpan(0, 0, 10)
            ).Invoke("test");

            Assert.Equal(
                "prepare, ask trigger, ask trigger, test",
                new Joined(", ", actions).AsString()
            );
        }

        [Fact]
        public void CancelsAfterTimeout()
        {
            Assert.Throws<ApplicationException>(
                () =>
                    new BowFunc<string>(
                        () => false,
                        () => { },
                        (str) => { },
                        new TimeSpan(0, 0, 0, 0, 100)
                    ).Invoke("test")
            );
        }

        [Fact]
        public void RejectsOnException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new BowFunc<string>(
                    () => throw new InvalidOperationException("fail"),
                    (str) => { }
                ).Invoke("test")
            );
        }
    }
}
