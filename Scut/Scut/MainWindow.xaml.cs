using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Win32;
using Scut.Properties;

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
            Rows.Clear();
            _watcher = new FileWatcher();
            _watcher.FileOpened += WatcherOnFileOpened;
            _watcher.RowsAdded += WatcherOnRowsAdded;

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

        private void WatcherOnRowsAdded(object sender, RowsAddedEventArgs rowsAddedEventArgs)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                // inject into grid
                foreach (var row in rowsAddedEventArgs.Rows)
                {
                    Rows.Add(RowViewModel.Parse(ScutSettings, row));
                }
            }));
        }

        private void WatcherOnFileOpened(object sender, RowsAddedEventArgs rowsAddedEventArgs)
        {
            var collection = new ObservableCollection<RowViewModel>();
            foreach (var row in rowsAddedEventArgs.Rows)
            {
                collection.Add(RowViewModel.Parse(ScutSettings, row));
            }

            Dispatcher.Invoke(() => Rows = collection);
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Settings.Default.MainWindowPlacement = this.GetPlacement();
            Settings.Default.Save();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.SetPlacement(Settings.Default.MainWindowPlacement);
        }

    }
}
