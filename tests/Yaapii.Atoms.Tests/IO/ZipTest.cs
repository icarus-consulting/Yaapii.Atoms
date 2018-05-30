using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;
using System.IO.Compression;

namespace Yaapii.Atoms.IO.Tests
{
    public class ZipTest
    {
        [Fact]
        public void HasData()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "ZipTest");
            try
            {
                Directory.CreateDirectory(folder);
                var newFile = File.Create(folder + "\\FileToZipOne.txt");
                newFile.Close();
                newFile = File.Create(folder + "\\FileToZipTwo.txt");
                newFile.Close();
                newFile = File.Create(folder + "\\FileToZipThree.txt");
                newFile.Close();

                var stream = new Zip(folder);
                Assert.InRange<long>(stream.Stream().Length, 1, long.MaxValue);

            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        [Fact]
        public void HasEntry()
        {
            string folder = Path.Combine(Directory.GetCurrentDirectory(), "ZipTest");
            try
            {
                Directory.CreateDirectory(folder);
                var newFile = File.Create(folder + "\\FileToZipOne.txt");
                newFile.Close();
                newFile = File.Create(folder + "\\FileToZipTwo.txt");
                newFile.Close();
                newFile = File.Create(folder + "\\FileToZipThree.txt");
                newFile.Close();

                var streamOfZipped = new Zip(folder);

                var archive = new ZipArchive(streamOfZipped.Stream());
                Assert.True(archive.GetEntry(folder + "\\FileToZipTwo.txt") != null);

            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }
    }
}
