using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.Func
{
    public sealed class IoCheckedFuncTest
    {
        [Fact]
        public void RethrowsCheckedToUncheckedException()
        {
            Exception exception = new Exception("fuck that's it");
            try
            {
                new IoCheckedFunc<bool, bool>(b =>
                {
                    throw exception;
                }
                ).Invoke(true);
            }
            catch (Exception ex)
            {
                Assert.True(typeof(IOException) == ex.GetType());
            }
        }
    }
}
