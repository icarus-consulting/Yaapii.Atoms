using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO
{
    public sealed class DirectoryOf : IEnumerable<string>
    {

        /// <summary>
        /// Path of the directory.
        /// </summary>
        private readonly IScalar<string> _dir;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dir">DirectoryInfo</param>
        public DirectoryOf(DirectoryInfo dir) : this(new ScalarOf<string>(() =>
        {
            return dir.FullName;
        })
        )
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a uri</param>
        public DirectoryOf(Uri file) : this(new ScalarOf<string>(() =>
            {
                if (file.Scheme != "file")
                {
                    throw
                     new ArgumentException(
                         new Formatted("'{0}' is not a directory.", file.ToString()).AsString()
                     );
                }
                return file.AbsolutePath;
            })
        )
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a path to directory.</param>
        public DirectoryOf(FileInfo file) : this(new ScalarOf<string>(file.Directory.FullName))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public DirectoryOf(string path) : this(new ScalarOf<string>(path))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public DirectoryOf(IScalar<string> path)
        {
            this._dir = new Scalar.Sticky<string>(() =>
            {
            var val = Path.GetFullPath(path.Value());
                try
                {
                    //check if path is a directory
                    if ((File.GetAttributes(val) & FileAttributes.Directory ) != FileAttributes.Directory)
                    {
                        throw new ArgumentException();
                    }
                }
                catch (ArgumentException)
                {
                    throw
                        new ArgumentException(
                            new Formatted("'{0}' is not a directory.", path.Value()
                        ).AsString()
                    );
                }

                return val;
            });
        }

        /// <summary>
        /// Enumerate directory and file paths as string.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return
                new Sorted<string>(
                    new Joined<string>(
                        Directory.EnumerateDirectories(_dir.Value()),
                        Directory.EnumerateFiles(_dir.Value())
                    )
                ).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}