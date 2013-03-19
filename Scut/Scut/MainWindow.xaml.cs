using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;

namespace Scut
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ScutSettings ScutSettings { get; set; }
        public IRowParser RowParser { get; set; }

        public ObservableCollection<RowViewModel> Rows { get; private set; }

        public MainWindow()
        {
            Rows = new ObservableCollection<RowViewModel>();
            InitializeComponent();

            ScutSettings = new ScutSettings();
            RowParser = new MockRowParser();

            CreateGrid(ScutSettings.ColumnSettings);

            RowParser.RowsParsed += ParserOnRowsAdded;
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
            // inject into grid
            foreach (var rowViewModel in rowsAddedEventArgs.ParsedRows)
            {
                Console.WriteLine(String.Join(", ", rowViewModel.Data));
                RowViewModel model = rowViewModel;
                Dispatcher.Invoke(() => Rows.Add(model));
            }
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { DefaultExt = ".log", Filter = "Log files (*.log)|*.log|Text files (*.txt)|*.txt" };

            if (dialog.ShowDialog() == true)
            {
                string filename = dialog.FileName;
                // TODO Load the file
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
    }
}
