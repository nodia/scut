using System;
using System.Windows;

namespace Scut
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ScutSettings ScutSettings { get; set; }
        public IRowParser RowParser { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ScutSettings = new ScutSettings();
            RowParser = new MockRowParser();

            var columns = ScutSettings.ColumnSettings;
            // create grid from columns

            RowParser.RowsAdded += ParserOnRowsAdded;
        }

        private void ParserOnRowsAdded(object sender, RowsAddedEventArgs rowsAddedEventArgs)
        {
            // inject into grid
            foreach (var rowViewModel in rowsAddedEventArgs.AddedRows)
            {
                Console.WriteLine(String.Join(", ", rowViewModel.Data));
            }
        }
    }
}
