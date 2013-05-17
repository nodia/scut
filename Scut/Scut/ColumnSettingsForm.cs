using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Scut
{
    public partial class ColumnSettingsForm : Form
    {
        private string _row;
        private ScutSettings _settings;

        public ColumnSettingsForm()
        {
            InitializeComponent();
        }

        public ColumnSettingsForm(string row, ScutSettings settings)
        {
            InitializeComponent();

            _row = row;
            _settings = settings;

            CreateGrid(_settings.ColumnSettings);

            PopulateHeaderEditors();

            textBoxDelimiter.Text = _settings.ColumnSeparator.ToString(CultureInfo.InvariantCulture);
            EvaluateRow();
        }

        private void PopulateHeaderEditors()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var column in _settings.ColumnSettings)
            {

                flowLayoutPanel1.Controls.Add(new TextBox
                    {
                        Text = column.Name,
                    });
            }
        }

        private void EvaluateRow(bool reEvalueteGrid = false)
        {
            dataGridViewColumnsExample.Rows.Clear();

            var model = RowViewModel.Parse(_settings, _row);

            if (reEvalueteGrid)
            {
                if (EvaluateGrid(model))
                {
                    CreateGrid(_settings.ColumnSettings);
                    PopulateHeaderEditors();
                }
            }

            var dgrow = new DataGridViewRow();
            dgrow.CreateCells(dataGridViewColumnsExample, model.Data);
            dgrow.DefaultCellStyle.BackColor = model.Color;

            dataGridViewColumnsExample.Rows.Add(dgrow);
        }

        private bool EvaluateGrid(RowViewModel model)
        {
            var columns = _settings.ColumnSettings;
            var data = model.Data;

            if (columns.Count < data.Length)
            {
                columns.AddRange(Enumerable.Range(1, (data.Length - columns.Count)).Select(i => new ColumnSetting{Name = "column"+i}));
                _settings.ColumnSettings = columns;

                return true;
            }

            if (columns.Count > data.Length)
            {
                columns = columns.Take(data.Length).ToList();
                _settings.ColumnSettings = columns;

                return true;
            }

            return false;
        }

        private void CreateGrid(IEnumerable<ColumnSetting> columns)
        {
            dataGridViewColumnsExample.Columns.Clear();
            foreach (var column in columns)
            {
                dataGridViewColumnsExample.Columns.Add(column.Name, column.Name);
            }

            foreach (DataGridViewColumn column in dataGridViewColumnsExample.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.Resizable = DataGridViewTriState.True;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void ButtonCancelClick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void TextBoxDelimiterLeave(object sender, System.EventArgs e)
        {
            if (textBoxDelimiter.Text.Length == 0)
            {
                MessageBox.Show(this, "Must specify delimiter!", "Invalid delimiter", MessageBoxButtons.OK,
                                MessageBoxIcon.Hand);
                return;
            }

            if (textBoxDelimiter.Text.Length > 1)
            {
                MessageBox.Show(this, "Using first character as delimiter", "Tried to be a funny guy, huh?",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            char delimiter = textBoxDelimiter.Text[0];

            _settings.ColumnSeparator = delimiter;
            EvaluateRow(true);
        }

        private void ButtonSaveClick(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
