using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yaapii.Atoms.Tests
{
    internal class Tidy : IAction
    {
        private readonly Action _task;
        private readonly Uri _file;

        public Tidy(Uri file, Action task)
        {
            _file = file;
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
            if (File.Exists(_file.AbsolutePath)) File.Delete(_file.AbsolutePath);
        }
    }
}
