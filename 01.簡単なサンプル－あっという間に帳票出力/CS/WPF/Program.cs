using System;
using System.Windows;

namespace Sample
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            Window1 window = new Window1();
            app.Run(window);
        }
    }
}
