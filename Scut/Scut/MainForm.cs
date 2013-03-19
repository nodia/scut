using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            foreach (DataGridViewColumn column in gridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Resizable = DataGridViewTriState.True;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void OpenFile(string filename)
        {
            gridView.Rows.Clear();
            _watcher = new FileWatcher();
            _watcher.FileOpened += WatcherOnRowsAdded;
            _watcher.RowsAdded += WatcherOnRowsAdded;

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
            BeginInvoke(new Action(() => AddRows(rowsAddedEventArgs.Rows)));
        }

        private void AddRows(IEnumerable<string> rows)
        {
            var rowArray = rows.Select(row =>
            {
                var model = RowViewModel.Parse(_settings, row);
                var dgrow = new DataGridViewRow();
                dgrow.CreateCells(gridView, model.Data);
                dgrow.DefaultCellStyle.BackColor = model.Color;
                dgrow.Visible = !model.Hidden;
                return dgrow;
            }).ToArray();
            gridView.Rows.AddRange(rowArray);

            if (!toolStripButtonScrollLock.Checked) return;

            for (int index = gridView.Rows.Count - 1; index > 0; index--)
            {
                var dataGridViewRow = gridView.Rows[index];
                if (dataGridViewRow.Visible)
                {
                    gridView.FirstDisplayedScrollingRowIndex = index;
                    return;
                }
            }
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

    }
}
