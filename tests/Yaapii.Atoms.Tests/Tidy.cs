using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests
{
    internal class Tidy : IAction
    {
        private readonly Action _task;
        private readonly IEnumerable<Uri> _files;

        public Tidy(Action task, params Uri[] files)
        {
            _files = files;
            _task = task;
        }

        public void Invoke()
        {
            Delete();
            try
            {
                _task.Invoke();
            }
            finally
            {
                Delete();
            }
        }

        private void Delete()
        {
            new Each<Uri>((uri) =>
                {
                    if (File.Exists(uri.AbsolutePath)) File.Delete(uri.AbsolutePath);
                },
                this._files
            ).Invoke();

        }
    }
}
