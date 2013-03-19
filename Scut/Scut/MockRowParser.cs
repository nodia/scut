using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Scut
{
    public class MockRowParser : IRowParser
    {
        private readonly Timer _timer;
        private readonly Random _random;

        public MockRowParser()
        {
            _random = new Random();

            _timer = new Timer { Interval = 1000 };
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            const int columnCount = 4;

            var args = new RowsParsedEventArgs();
            args.AddedRows = new List<RowViewModel>();
            foreach (var i in Enumerable.Range(0, _random.Next(1, 3)))
            {
                var rowViewModel = new RowViewModel { Data = Enumerable.Range(0, columnCount).Select(j => RandomString()).ToList() };
                args.AddedRows.Add(rowViewModel);
            }
            OnRowsAdded(args);
        }

        private string RandomString(int length = 8)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[_random.Next(s.Length)])
                          .ToArray());
        }

        public event EventHandler<RowsParsedEventArgs> RowsParsed;

        protected virtual void OnRowsAdded(RowsParsedEventArgs e)
        {
            EventHandler<RowsParsedEventArgs> handler = RowsParsed;
            if (handler != null) handler(this, e);
        }
    }
}