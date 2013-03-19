using System.Collections.Generic;

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
        }

        public List<ColumnSetting> ColumnSettings { get; set; }

        public char ColumnSeparator { get; set; }
    }
}