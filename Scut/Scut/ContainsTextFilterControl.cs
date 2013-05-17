using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Scut
{
    public partial class ContainsTextFilterControl : UserControl
    {
        private ContainsTextFilter _filter;
        private bool _loaded;

        public ContainsTextFilterControl()
        {
            InitializeComponent();
            RbColorCheckedChanged(null, null);
        }

        private void FilterControlLoad(object sender, EventArgs e)
        {
            var propInfoList = typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (var propertyInfo in propInfoList)
            {
                cbColor.Items.Add(propertyInfo.Name);
            }

            _loaded = true;
            if (_filter != null)
            {
                SetValues();
            }
        }

        private void ComboBox1DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            var colorName = cbColor.Items[e.Index].ToString();
            Color color = Enum.GetNames(typeof(KnownColor)).Contains(colorName) ? Color.FromName(colorName) : GetArgbColor(colorName);

            var font = DefaultFont;
            var defaultForeColor = new SolidBrush(DefaultForeColor);

            e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);
            g.DrawString(colorName, font, defaultForeColor, rect.X, rect.Top);
        }

        private Color GetArgbColor(string colorName)
        {
            try
            {
                colorName = colorName.TrimStart('#');
                int a = 255;
                if (colorName.Length == 8)
                {
                    a = Convert.ToInt32(colorName.Substring(0, 2), 16);
                    colorName = colorName.Substring(2);
                }

                var r = Convert.ToInt32(colorName.Substring(0, 2), 16);
                var g = Convert.ToInt32(colorName.Substring(2, 2), 16);
                var b = Convert.ToInt32(colorName.Substring(4, 2), 16);
                return Color.FromArgb(a, r, g, b);
            }
            catch
            {
                return Color.Red;
            }
        }

        private void BtnRemoveClick(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }

        private void RbColorCheckedChanged(object sender, EventArgs e)
        {
            cbColor.Enabled = rbColor.Checked;
        }

        public ContainsTextFilter Filter { 
            get
            {
                if (_filter == null)
                {
                    _filter = new ContainsTextFilter();
                }
                _filter.Color = rbColor.Checked ? GetSelectedColor() : null;
                _filter.Hide = rbHide.Checked;
                _filter.IgnoreCase = cbIgnoreCase.Checked;
                _filter.Text = tbText.Text;
                return _filter;
            } 
            set
            {
                _filter = value;
                if (_loaded)
                {
                    SetValues();
                }
            } 
        }

        private void SetValues()
        {
            tbText.Text = _filter.Text;
            rbColor.Checked = _filter.Color.HasValue;
            if (_filter.Color.HasValue)
            {
                if (_filter.Color.Value.IsKnownColor)
                {
                    cbColor.SelectedItem = _filter.Color.Value.ToKnownColor().ToString();
                }
                else
                {
                    cbColor.Text = "#";
                    if (_filter.Color.Value.A != 255)
                    {
                        cbColor.Text += HexString(_filter.Color.Value.A);
                    }
                    cbColor.Text += HexString(_filter.Color.Value.R) + HexString(_filter.Color.Value.G) + HexString(_filter.Color.Value.B);
                }
            }
            rbHide.Checked = _filter.Hide;
        }

        private string HexString(byte b)
        {
            var hexString = b.ToString("X");
            hexString = (hexString.Length % 2 == 0 ? "" : "0") + hexString;
            return hexString;
        }

        private Color? GetSelectedColor()
        {
            var colorName = cbColor.SelectedItem != null ? cbColor.SelectedItem.ToString() : cbColor.Text;
            if (String.IsNullOrWhiteSpace(colorName))
            {
                return null;
            }

            Color color = Enum.GetNames(typeof(KnownColor)).Contains(colorName) ? Color.FromName(colorName) : GetArgbColor(colorName);
            return color;
        }
    }
}
