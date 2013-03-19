using System;
using System.Linq;

namespace Scut
{
    class RowParser : IRowParser
    {
        private readonly ScutSettings _settings;
        private readonly FileWatcher _fileWatcher;

        public RowParser(ScutSettings settings, FileWatcher fileWatcher)
        {
            _settings = settings;
            _fileWatcher = fileWatcher;
            _fileWatcher.RowsAdded += FileWatcherOnRowsAdded;
        }

        private void FileWatcherOnRowsAdded(object sender, RowsAddedEventArgs rowsAddedEventArgs)
        {
            OnRowsParsed(new RowsParsedEventArgs { ParsedRows = rowsAddedEventArgs.Rows.Select(ParseRow).ToList() });
        }

        private RowViewModel ParseRow(string row)
        {
            var rvm = new RowViewModel { Raw = row };
            rvm.Parse(_settings);

            return rvm;
        }

        public event EventHandler<RowsParsedEventArgs> RowsParsed;

        protected virtual void OnRowsParsed(RowsParsedEventArgs e)
        {
            EventHandler<RowsParsedEventArgs> handler = RowsParsed;
            if (handler != null) handler(this, e);
        }
    }
}