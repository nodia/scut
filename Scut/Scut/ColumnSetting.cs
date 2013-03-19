using System.Runtime.Serialization;

namespace Scut
{
    [DataContract]
    public class ColumnSetting
    {
        [DataMember]
        public string Name { get; set; }
    }
}