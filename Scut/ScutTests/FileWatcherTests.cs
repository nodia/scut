using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using Scut;

namespace ScutTests
{
    [TestFixture]
    public class FileWatcherTests
    {
        [Test]
        public void TestFileWatcherReturnsFalseOnInvalidFile()
        {
            var watcher = new FileWatcher();
            Assert.IsFalse(watcher.Watch("sprölölö"));
            Assert.IsFalse(watcher.Watch(@"C:\Windows"));
        }

        [Test]
        public void TestFileWatcherTailsFile()
        {
            string filename = Path.GetTempFileName();

            var rows = new List<string>();
            using (TextWriter writer = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write)))
            {
                writer.WriteLine("moi");

                var watcher = new FileWatcher();
                watcher.RowsAdded += (s, r) => rows.AddRange(r.Rows);
                Assert.IsTrue(watcher.Watch(filename));

                writer.WriteLine("Terve" + Environment.NewLine + "hello");
                writer.Write("äöä morjens äöä");
            }
            Thread.Sleep(100);
            CollectionAssert.AreEqual(new[] { "moi", "Terve", "hello", "äöä morjens äöä" }, rows);
        }
    }
}
