using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace Scut
{
    [DataContract]
    public class ScutSettings
    {
        public ScutSettings()
        {
        }

        public ScutSettings(ScutSettings settings)
        {
            ColumnSeparator = settings.ColumnSeparator;
            ColumnSettings = settings.ColumnSettings;
            Filters = settings.Filters;
        }

        public static ScutSettings CreateDefaults()
        {
            return new ScutSettings {
                ColumnSettings = new List<ColumnSetting>
                {
                    new ColumnSetting { Name = "Timestamp" },
                    new ColumnSetting { Name = "Severity" },
                    new ColumnSetting { Name = "Tag" },
                    new ColumnSetting { Name = "Message" },
                },

                ColumnSeparator = '|',

                Filters = new List<IFilter>
                {
                    new ContainsTextFilter { Color = Color.GreenYellow, Text = "packet" },
                    new ContainsTextFilter { Hide = true, Text = "Start" }
                }
            };
        }

        [DataMember]
        public List<ColumnSetting> ColumnSettings { get; set; }

        [DataMember]
        public List<IFilter> Filters { get; private set; }

        [DataMember]
        public char ColumnSeparator { get; set; }
    }
}