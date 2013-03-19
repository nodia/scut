using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Scut
{
    public class FileWatcher : IDisposable
    {
        private FileSystemWatcher _watcher;
        private StreamReader _reader;
        public event EventHandler<RowsAddedEventArgs> RowsAdded;
        private readonly string[] _splitters = new[] { Environment.NewLine, "\n" };

        public bool Watch(string path)
        {
            if (_watcher != null)
            {
                _watcher.Dispose();
            }

            if (_reader != null)
            {
                _reader.Dispose();
            }

            try
            {
                if (!File.Exists(path))
                {
                    return false;
                }
                var pathName = Path.GetDirectoryName(Path.GetFullPath(path));
                var fileName = Path.GetFileName(path);
                if (pathName == null || fileName == null)
                {
                    return false;
                }

                _watcher = new FileSystemWatcher(pathName, fileName);
                _reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete));

                Task.Factory.StartNew(ReadToEnd);
            }
            catch (ArgumentException)
            {
                return false;
            }

            _watcher.Changed += WatcherChanged;
            _watcher.EnableRaisingEvents = true;
            return true;
        }

        private void ReadToEnd()
        {
            var strings = _reader.ReadToEnd().Split(_splitters, StringSplitOptions.None).ToList();
            if (String.IsNullOrEmpty(strings.Last()))
            {
                strings = strings.Take(strings.Count - 1).ToList();
            }
            OnRowsAdded(new RowsAddedEventArgs { Rows = strings });
        }

        protected virtual void OnRowsAdded(RowsAddedEventArgs e)
        {
            EventHandler<RowsAddedEventArgs> handler = RowsAdded;
            if (handler != null) handler(this, e);
        }

        private void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                ReadToEnd();
            }

            // TODO: handle deletions etc.
        }

        public void Dispose()
        {
            if (_watcher != null)
            {
                _watcher.Dispose();
                _watcher = null;
            }

            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }
        }
    }
}
