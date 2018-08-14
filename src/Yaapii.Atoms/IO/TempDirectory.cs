using System;
using System.IO;

namespace Yaapii.Atoms.IO
{
    public sealed class TempDirectory : IScalar<DirectoryInfo>, IDisposable
    {
        private readonly Uri path;

        public TempDirectory(string path) : this(
            new Uri(path)
        )
        { }

        public TempDirectory(Uri path)
        {
            this.path = path;
        }

        public void Dispose()
        {
            if (Directory.Exists(path.AbsolutePath))
            {
                DeleteDirectory(path.AbsolutePath);
            }
        }

        public DirectoryInfo Value()
        {
            if (!Directory.Exists(this.path.AbsolutePath))
            {
                Directory.CreateDirectory(this.path.AbsolutePath);
            }
            return new DirectoryInfo(this.path.AbsolutePath);
        }

        private void DeleteDirectory(string path)
        {
            foreach (string subDir in Directory.GetDirectories(path))
            {
                DeleteDirectory(subDir);
            }
            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
            }
        }
    }
}
