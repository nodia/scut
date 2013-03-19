using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

            RowParser.RowsAdded += ParserOnRowsAdded;
        }

        private void CreateGrid(List<ColumnSetting> columns)
        {
            var gridView = (GridView) logView.View;

            for (int i = 0; i < columns.Count; i++)
            {
                gridView.Columns.Add(new GridViewColumn
                {
                    Header = columns[i].Name,
                    DisplayMemberBinding = new Binding(string.Format("Data[{0}]", i))
                });
            }
        }

        private void ParserOnRowsAdded(object sender, RowsAddedEventArgs rowsAddedEventArgs)
        {
            // inject into grid
            foreach (var rowViewModel in rowsParsedEventArgs.AddedRows)
            {
                Console.WriteLine(String.Join(", ", rowViewModel.Data));
                RowViewModel model = rowViewModel;
                Dispatcher.Invoke(() => Rows.Add(model));
            }
        }
    }
}
