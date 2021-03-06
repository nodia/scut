﻿using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
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

        public string[] Data { get; set; }

        public string Raw { get; set; }

        public static RowViewModel Parse(ScutSettings settings, string row)
        {
            var rvm = new RowViewModel
            {
                Raw = row,
                Data = row.Split(settings.ColumnSeparator)
            };

            foreach (var filter in settings.Filters)
            {
                filter.Filter(rvm);
            }

            if (rvm.Data.Length != settings.ColumnSettings.Count)
            {
                rvm.Color = Color.DarkSalmon;
            }

            return rvm;
        }
    }
}
