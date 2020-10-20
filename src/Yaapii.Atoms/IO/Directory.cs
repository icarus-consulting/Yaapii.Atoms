using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        /// include all files from sub directories
        /// </summary>
        private readonly IScalar<bool> _recursive;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dir">DirectoryInfo</param>
        public DirectoryOf(DirectoryInfo dir) : this(dir, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dir">DirectoryInfo</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(DirectoryInfo dir, bool recursive) : this(new Live<string>(() =>
            {
                return dir.FullName;
            }),
            new Live<bool>(recursive)
        )
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a uri</param>
        public DirectoryOf(Uri file) : this(file, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a uri</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(Uri file, bool recursive) : this(new Live<string>(() =>
            {
                if (file.Scheme != "file")
                {
                    throw
                     new ArgumentException(
                         new Formatted("'{0}' is not a directory.", file.ToString()).ToString()
                     );
                }
                return file.AbsolutePath;
            }),
            new Live<bool>(recursive)
        )
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a path to directory.</param>
        public DirectoryOf(FileInfo file) : this(file, false)
        { }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="file">File as a path to directory.</param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(FileInfo file, bool recursive) : this(new Live<string>(file.Directory.FullName), new Live<bool>(recursive))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public DirectoryOf(string path) : this(path, false)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(string path, bool recursive) : this(new Live<string>(path), new Live<bool>(recursive))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        public DirectoryOf(IScalar<string> path) : this(path, new False())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive">include all files from sub directories</param>
        public DirectoryOf(IScalar<string> path, IScalar<bool> recursive)
        {
            this._dir = new ScalarOf<string>(() =>
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
                        ).ToString()
                    );
                }

                return val;
            });
            this._recursive = recursive;
        }

        /// <summary>
        /// Enumerate directory and file paths as string.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return new Ternary<bool, IEnumerator<string>>(
                this._recursive,
                new Sorted<string>(
                    Directory.EnumerateFiles(_dir.Value(), "*", SearchOption.AllDirectories)
                    
                ).GetEnumerator(),
                new Sorted<string>(
                    new Joined<string>(
                        Directory.EnumerateDirectories(_dir.Value()),
                        Directory.EnumerateFiles(_dir.Value())
                    )
                ).GetEnumerator()
            ).Value();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}