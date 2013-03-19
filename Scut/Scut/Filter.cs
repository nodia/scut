using System;
using System.Drawing;
using System.Linq;

namespace Scut
{
    public class ContainsTextFilter : IFilter
    {
        public ContainsTextFilter()
        {
            StringComparison = StringComparison.InvariantCultureIgnoreCase;
        }

        public string Text { get; set; }

        public bool Hide { get; set; }

        public Color Color { get; set; }

        public StringComparison StringComparison { get; set; }

        public void Filter(RowViewModel row)
        {
            if (row.Raw.IndexOf(Text, StringComparison) >= 0)
            {
                row.Color = Color;
                row.Hidden = true;
            }
        }
    }
}
