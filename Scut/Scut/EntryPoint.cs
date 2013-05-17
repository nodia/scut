using System;
using System.Windows.Forms;

namespace Scut
{
    internal class EntryPoint
    {
        [STAThread]
        public static void Main(string[] args)
        {
            //App.Main();
            var window = new MainForm();
            Application.EnableVisualStyles();
            Application.Run(window);
        }
    }
}
