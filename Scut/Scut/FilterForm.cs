using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Scut
{
    public partial class FilterForm : Form
    {
        private bool _canceled;

        public FilterForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.OK;
        }

        private void BtnAddClick(object sender, System.EventArgs e)
        {
            filterPanel.Controls.Add(new ContainsTextFilterControl());
        }

        private void BtnOkClick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void BtnCancelClick(object sender, System.EventArgs e)
        {
            _canceled = true;
            Close();
        }

        public List<IFilter> GetFilters()
        {
            return filterPanel.Controls.OfType<ContainsTextFilterControl>().Select(control => (IFilter)control.Filter).ToList();
        }

        public void SetFilters(List<IFilter> filters)
        {
            foreach (var filter in filters.OfType<ContainsTextFilter>())
            {
                filterPanel.Controls.Add(new ContainsTextFilterControl { Filter = filter });
            }
        }

        private void FilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = _canceled ? DialogResult.Cancel : DialogResult.OK;
        }
    }
}
