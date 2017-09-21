using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public async Task RunsInBackgroundWithoutFuture()
        {
            var future = await
                new AsyncFunc<bool, string>(
                    input =>
                    {
                        Thread.Sleep(new TimeSpan(0, 0, 0, 0, 100)); //sleep for a second
                        return "done!";
                    }
                ).Invoke(true);

            Assert.True(future == "done!","cannot await future");
        }
    }
}
