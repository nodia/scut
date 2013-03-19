using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using DataFormats = System.Windows.DataFormats;

namespace Scut
{
    public partial class MainForm : Form
    {
        private ScutSettings _settings;
        private FileWatcher _watcher;

        public MainForm()
        {
            InitializeComponent();

            _settings = new ScutSettings();

            CreateGrid(_settings.ColumnSettings);
        }

        private void CreateGrid(IEnumerable<ColumnSetting> columns)
        {
            foreach (var column in columns)
            {
                gridView.Columns.Add(column.Name, column.Name);
            }
        }

        private void OpenFile(string filename)
        {
            gridView.Rows.Clear();
            _watcher = new FileWatcher();
            //_watcher.FileOpened += WatcherOnFileOpened;
            _watcher.RowsAdded += WatcherOnRowsAdded;
            _watcher.FileOpened += WatcherOnRowsAdded;

            var success = _watcher.Watch(filename);
            if (success)
            {
                Text = "Tailing: " + Path.GetFullPath(filename);
            }
            else
            {
                Text = "Error opening: " + Path.GetFullPath(filename);
            }
        }

        private void OpenToolStripMenuItemClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog { DefaultExt = ".log", Filter = "Log files (*.log)|*.log|Text files (*.txt)|*.txt|All files|*.*" };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                OpenFile(filename);
            }
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void WatcherOnRowsAdded(object sender, RowsAddedEventArgs rowsAddedEventArgs)
        {
            BeginInvoke(new Action(() =>
            {
                // inject into grid
                foreach (var row in rowsAddedEventArgs.Rows)
                {
                    AddRow(RowViewModel.Parse(_settings, row));
                }
            }));
        }

        private void AddRow(RowViewModel row)
        {
            var dgrow = new DataGridViewRow();
            dgrow.CreateCells(gridView, row.Data);
            dgrow.DefaultCellStyle.BackColor = row.Color;
            gridView.Rows.Add(dgrow);
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.MainWindowPlacement = WindowPlacement.GetPlacement(Handle);
            Properties.Settings.Default.Save();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            WindowPlacement.SetPlacement(Handle, Properties.Settings.Default.MainWindowPlacement);
        }

        private void MainForm_DragOver(object sender, DragEventArgs e)
        {
            var droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (droppedFilePaths == null)
            {
                e.Effect = DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (droppedFilePaths == null)
            {
                return;
            }

            var file = droppedFilePaths[0];
            OpenFile(file);
        }

        /*
        private void WatcherOnFileOpened(object sender, RowsAddedEventArgs rowsAddedEventArgs)
        {
            var collection = new ObservableCollection<RowViewModel>();
            foreach (var row in rowsAddedEventArgs.Rows)
            {
                collection.Add(RowViewModel.Parse(ScutSettings, row));
            }

            Dispatcher.Invoke(() => Rows = collection);
        }
         */
    }
}
