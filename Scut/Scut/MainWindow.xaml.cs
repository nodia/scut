using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;

namespace Scut
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileWatcher _watcher;
        public ScutSettings ScutSettings { get; set; }

        public ObservableCollection<RowViewModel> Rows { get; private set; }

        public MainWindow()
        {
            Rows = new ObservableCollection<RowViewModel>();
            InitializeComponent();

            ScutSettings = new ScutSettings();

            CreateGrid(ScutSettings.ColumnSettings);
        }

        private void CreateGrid(List<ColumnSetting> columns)
        {
            var gridView = (GridView) logView.View;

            for (int i = 0; i < columns.Count; i++)
            {
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = columns[i].Name,
                    DisplayMemberBinding = new Binding(string.Format("Data[{0}]", i)),
                });
            }
        }

        private void ParserOnRowsAdded(object sender, RowsParsedEventArgs rowsAddedEventArgs)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                // inject into grid
                foreach (var rowViewModel in rowsAddedEventArgs.ParsedRows)
                {
                    Rows.Add(rowViewModel);
                }
            }));
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { DefaultExt = ".log", Filter = "Log files (*.log)|*.log|Text files (*.txt)|*.txt|All files|*.*" };

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                OpenFile(filename);
            }
        }

        private void OpenFile(string filename)
        {
            _watcher = new FileWatcher();
            var parser = new RowParser(ScutSettings, _watcher);
            parser.RowsParsed += ParserOnRowsAdded;

            var success = _watcher.Watch(filename);
            if (success)
            {
                Title = "Tailing: " + Path.GetFullPath(filename);
            }
            else
            {
                Title = "Error opening: " + Path.GetFullPath(filename);
            }
        }

        private void CommandBinding_OnCanOpenExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }

        private void MainWindow_OnDrop(object sender, DragEventArgs e)
        {
            var droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (droppedFilePaths == null)
            {
                return;
            }

            var file = droppedFilePaths[0];
            OpenFile(file);
        }

        private void CanDrop(object sender, DragEventArgs e)
        {
            var droppedFilePaths = e.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (droppedFilePaths == null)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

    }
}
