using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class AsyncFuncTest
    {
        [Fact]
        public void RunsInBackground()
        {
            var future = 

                new AsyncFunc<bool, string>(
                    input =>
                    {
                        Thread.Sleep(new TimeSpan(1, 0, 0, 0)); //sleep for a day
                        return "done!";
                    }
                ).Invoke(true);

            Assert.True(!future.IsCompleted);

        }

        [Fact]
        public void RunsInBackgroundWithoutFuture()
        {
            var future =
                new AsyncFunc<bool, string>(
                    input =>
                    {
                        Thread.Sleep(new TimeSpan(0, 0, 0, 0, 100)); //sleep for a second
                        return "done!";
                    }
                ).Invoke(true);

            Thread.Sleep(1000);
            Assert.True(future.IsCompleted,"cannot await future");
            Assert.True(future.Result == "done!");
        }
    }
}
