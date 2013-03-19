using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Scut.Annotations;

namespace Scut
{
    public class RowViewModel : INotifyPropertyChanged
    {
        private bool _hidden;
        private Color _color;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Hidden
        {
            get { return _hidden; }
            set
            {
                if (value.Equals(_hidden)) return;
                _hidden = value;
                OnPropertyChanged();
            }
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                if (value.Equals(_color)) return;
                _color = value;
                OnPropertyChanged();
            }
        }

        public IList<string> Data { get; set; }

        public string Raw { get; set; }

        public void Parse(ScutSettings settings)
        {
            Data = Raw.Split(settings.ColumnSeparator);

            foreach (var filter in settings.Filters)
            {
                filter.Filter(this);
            }
        }
    }
}
