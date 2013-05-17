using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace Scut
{
    [DataContract]
    public class ContainsTextFilter : IFilter
    {
        public ContainsTextFilter()
        {
            IgnoreCase = true;
        }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public bool Hide { get; set; }

        [DataMember]
        public Color? Color { get; set; }

        [DataMember]
        public bool IgnoreCase { get; set; }

        public void Filter(RowViewModel row)
        {
            if (row.Raw.IndexOf(Text, IgnoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture) >= 0)
            {
                if (Color.HasValue)
                    row.Color = Color.Value;
                row.Hidden = Hide;
            }
        }
    }
}
