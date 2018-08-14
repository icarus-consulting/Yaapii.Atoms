using System.IO;
using Xunit;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class TempDirectoryTest
    {
        [Fact]
        public void CreatesDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            try
            {
                var td = new TempDirectory(path).Value();
                Assert.True(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void DeletesEmptyDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            try
            {
                using (var td = new TempDirectory(path))
                {
                    td.Value();
                }
                Assert.False(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }

        [Fact]
        public void DeletesNotEmptyDirectory()
        {
            var path = Path.Combine(Path.GetTempPath(), "SunnyDirectory");
            using (var td = new TempDirectory(path))
            {
                td.Value();
                File.WriteAllBytes(Path.Combine(path, "rainig.txt"), new byte[] { 0xAB });
            }
            try
            {
                Assert.False(
                    Directory.Exists(path)
                );
            }
            finally
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
        }
    }
}
