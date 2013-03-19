using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scut
{
    public class FileWatcher
    {
        public event EventHandler<RowsAddedEventArgs> RowsAdded;
        public event EventHandler<RowsAddedEventArgs> FileOpened;

        private readonly string[] _splitters = new[] { Environment.NewLine, "\n" };
        protected const int SleepTime = 100;

        private bool _running;
        private StreamReader _reader;

        public long OldLinesCount { get; set; }

        public FileWatcher()
        {
            OldLinesCount = -1;
        }

        public bool Watch(string path)
        {
            _running = false;
            try
            {
                if (!File.Exists(path))
                {
                    return false;
                }

                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 1024, true);
                if (OldLinesCount > -1)
                {
                    fileStream.Seek(Math.Max(0, fileStream.Length - OldLinesCount), SeekOrigin.Begin);
                }
                _reader = new StreamReader(fileStream);

                _running = true;
                ReadToEnd(true);

                Task.Factory.StartNew(Run);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        private void ReadToEnd(bool fileOpened = false)
        {
            var strings = _reader.ReadToEnd().Split(_splitters, StringSplitOptions.None).ToList();

            if (strings.Count == 0) return;

            if (String.IsNullOrEmpty(strings.Last()))
            {
                strings = strings.Take(strings.Count - 1).ToList();
            }

            if (strings.Count == 0) return;

            if (fileOpened)
            {
                OnFileOpened(new RowsAddedEventArgs { Rows = strings });
            }
            else
            {
                OnRowsAdded(new RowsAddedEventArgs { Rows = strings });
            }
        }

        public void Stop()
        {
            _running = false;
        }

        private void Run()
        {
            try
            {
                while (_running)
                {
                    try
                    {
                        Thread.Sleep(SleepTime);
                        ReadToEnd();
                    }
                    catch (ThreadInterruptedException)
                    {
                        Thread.Sleep(50);
                    }
                    catch (Exception)
                    {
                        _running = false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                if (_reader != null)
                {
                    _reader.Dispose();
                    _reader = null;
                }
            }
        }

        protected virtual void OnRowsAdded(RowsAddedEventArgs e)
        {
            EventHandler<RowsAddedEventArgs> handler = RowsAdded;
            if (handler != null) handler(this, e);
        }

        protected virtual void OnFileOpened(RowsAddedEventArgs e)
        {
            EventHandler<RowsAddedEventArgs> handler = FileOpened;
            if (handler != null) handler(this, e);
        }
    }
}
