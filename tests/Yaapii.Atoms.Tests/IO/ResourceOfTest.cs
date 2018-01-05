using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;
using Yaapii.IO;

namespace Yaapii.Atoms.Tests.IO
{
    public class ResourceOfTest
    {
        [Fact]
        public void FindsResourceInAssembly()
        {
            Assert.Equal(
                "Hello from Embedded!",
                new TextOf(
                    new ResourceOf("IO/Resources/test.txt", Assembly.GetExecutingAssembly())
                ).AsString()
            );
        }

        [Fact]
        public void FindsResourceByType()
        {
            Assert.Equal(
                "Hello from Embedded!",
                new TextOf(
                    new ResourceOf("IO/Resources/test.txt", this.GetType())
                ).AsString()
            );
        }
    }
}