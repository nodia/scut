using System.Collections.Generic;
using System.Drawing;

namespace Scut
{
    public class ScutSettings
    {
        public ScutSettings()
        {
            ColumnSettings = new List<ColumnSetting>
            {
                new ColumnSetting { Name = "Timestamp" },
                new ColumnSetting { Name = "Severity" },
                new ColumnSetting { Name = "Tag" },
                new ColumnSetting { Name = "Message" },
            };

            ColumnSeparator = '|';
            Filters = new List<IFilter>
            {
                new ContainsTextFilter { Color = Color.GreenYellow, Text = "packet" },
                new ContainsTextFilter { Hide = true, Text = "repository" }
            };
        }

        public List<ColumnSetting> ColumnSettings { get; set; }

        public List<IFilter> Filters { get; private set; } 

        public char ColumnSeparator { get; set; }
    }
}