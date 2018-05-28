using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO;

namespace Yaapii.Atoms.IO.Tests
{
    public class ZipTest
    {
        [Fact]
        public void HasData()
        {
            string folderPath = Path.GetTempPath() + "\\" + "ZipTestFolder";
            try
            {
                var folder = Directory.CreateDirectory(folderPath);
                var newFile = File.Create(folder.FullName + "\\FileToZipOne.txt");
                newFile.Close();
                newFile = File.Create(folder.FullName + "\\FileToZipTwo.txt");
                newFile.Close();
                newFile = File.Create(folder.FullName + "\\FileToZipThree.txt");
                newFile.Close();

                var streamOfZipped = new Zip(folder);
                Assert.InRange<long>(streamOfZipped.Stream().Length, 1, long.MaxValue);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Directory.Delete(folderPath, true);
            }
        }

        [Fact]
        public void EntryExistsInStream()
        {
            string folderPath = Path.GetTempPath() + "\\" + "ZipTestFolder";
            try
            {
                var folder = Directory.CreateDirectory(folderPath);
                var newFile = File.Create(folder.FullName + "\\FileToZipOne.txt");
                newFile.Close();
                newFile = File.Create(folder.FullName + "\\FileToZipTwo.txt");
                newFile.Close();
                newFile = File.Create(folder.FullName + "\\FileToZipThree.txt");
                newFile.Close();

                var streamOfZipped = new Zip(folder);

                var archive = new System.IO.Compression.ZipArchive(streamOfZipped.Stream());
                Assert.True(archive.GetEntry(folder.FullName + "\\FileToZipTwo.txt") != null);

            }
            finally
            {
                Directory.Delete(folderPath, true);
            }
        }
    }
}
