using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using DataFormats = System.Windows.DataFormats;

namespace Scut
{
    public partial class MainForm : Form
    {
        private ScutSettings _settings;
        private FileWatcher _watcher;
        private string _lastSearchString;

        private string _exampleRow;
        private string _fileName;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                _textBoxSearch.Focus();
                return true;
            }

            if (keyData == Keys.F3)
            {
                Search(_lastSearchString);
            }

            return base.ProcessCmdKey(ref msg, keyData);
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
                _fileName = dialog.FileName;
                OpenFile(_fileName);
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
            _exampleRow = rows.First();
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

            SerializeSettings();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            WindowPlacement.SetPlacement(Handle, Properties.Settings.Default.MainWindowPlacement);

            DeserializeSettings();
            CreateGrid(_settings.ColumnSettings);
        }

        private void SerializeSettings()
        {
            const string fileName = "scutsettings.xml";
            try
            {
                var serializer = new DataContractSerializer(typeof(ScutSettings), new[] { typeof(ContainsTextFilter) });
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "\t"
                };

                using(var writer = XmlWriter.Create(new FileStream(fileName, FileMode.Create, FileAccess.Write), settings))
                {
                    serializer.WriteObject(writer, _settings);
                }
            }
            catch
            {
                
            }
        }

        private void DeserializeSettings()
        {
            try
            {
                const string fileName = "scutsettings.xml";
                if (File.Exists(fileName))
                {
                    var serializer = new DataContractSerializer(typeof(ScutSettings), new[] { typeof(ContainsTextFilter) });
                    using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        _settings = serializer.ReadObject(stream) as ScutSettings;
                    }
                }
            }
            catch
            {
                
            }

            if (_settings == null)
            {
                _settings = ScutSettings.CreateDefaults();
            }
        }

        private void MainFormDragOver(object sender, DragEventArgs e)
        {
            var droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            e.Effect = droppedFilePaths == null ? DragDropEffects.None : DragDropEffects.All;
        }

        private void MainFormDragDrop(object sender, DragEventArgs e)
        {
            var droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (droppedFilePaths == null)
            {
                return;
            }

            _fileName = droppedFilePaths[0];
            OpenFile(_fileName);
        }

        private void ColumnsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var newSettings = new ScutSettings(_settings);
            using (var columnsettings = new ColumnSettingsForm(_exampleRow, newSettings))
            {
                if (columnsettings.ShowDialog(this) == DialogResult.OK)
                {
                    _settings = newSettings;
                    CreateGrid(_settings.ColumnSettings);
                    OpenFile(_fileName);
                }
            }
        }

        private void Search(string text)
        {
            _lastSearchString = text;

            if (text == null)
            {
                return;
            }

            if (gridView.Rows.Count == 0)
            {
                return;
            }

            Regex regex = null;
            if (btnRegex.Checked)
            {
                var options = btnCaseSensitive.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                regex = new Regex(text, options);
            }

            var comparisonType = btnCaseSensitive.Checked ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;

            int firstRow = gridView.SelectedCells.Count == 0 ? gridView.FirstDisplayedScrollingRowIndex : gridView.SelectedCells[0].RowIndex;
            gridView.ClearSelection();

            for (int i = firstRow + 1;; i++)
            {
                if (i == firstRow)
                {
                    break;
                }

                if (i >= gridView.Rows.Count)
                {
                    i = 0;
                }

                var row = gridView.Rows[i];
                if (!row.Visible)
                {
                    continue;
                }

                for (int j = 0; j < row.Cells.Count; j++)
                {
                    var cell = row.Cells[j];
                    if (cell.Value == null)
                    {
                        continue;
                    }

                    var s = cell.Value.ToString();

                    if (regex != null ? !regex.IsMatch(s) : s.IndexOf(text, comparisonType) < 0)
                    {
                        continue;
                    }

                    cell.Selected = true;
                    var displayCount = gridView.DisplayedRowCount(false);
                    if (i < gridView.FirstDisplayedScrollingRowIndex || i >= gridView.FirstDisplayedScrollingRowIndex + displayCount)
                    {
                        var rowIndex = FindVisibleRow(i - displayCount / 2);
                        if (rowIndex >= 0) // rowIndex is -1 if no visible lines found
                        {
                            gridView.FirstDisplayedScrollingRowIndex = rowIndex;
                        }
                    }

                    return;
                }
            }
        }

        private int FindVisibleRow(int i)
        {
            var rowIndex = Math.Max(0, i);
            int dir = -1;
            while (!gridView.Rows[rowIndex].Visible)
            {
                rowIndex += dir;
                if (rowIndex == -1)
                {
                    dir = 1;
                    rowIndex = 0;
                }

                if (rowIndex >= gridView.Rows.Count)
                {
                    return -1;
                }
            }
            return rowIndex;
        }

        private void TextBoxSearchKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            Search(_textBoxSearch.Text);
        }

        private void BtnFiltersClick(object sender, EventArgs e)
        {
            var filterForm = new FilterForm();
            filterForm.SetFilters(_settings.Filters);
            var result = filterForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var filters = filterForm.GetFilters();
                _settings.Filters.Clear();
                _settings.Filters.AddRange(filters);
                SerializeSettings();
                ReloadSettings();
            }
        }

        private void ReloadSettings()
        {
            if (_fileName != null)
            {
                OpenFile(_fileName);
            }
        }
    }
}
