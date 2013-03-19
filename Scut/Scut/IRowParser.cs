using System;

namespace Scut
{
    public interface IRowParser
    {
        event EventHandler<RowsParsedEventArgs> RowsParsed;
    }
}