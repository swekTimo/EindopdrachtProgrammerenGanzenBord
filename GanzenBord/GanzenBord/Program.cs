using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GanzenBord
{
    class Program
    {
        private static ServerGanzenbord Server;
        private static ClientGanzenbord Client;
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Om een server op te starten typ dan 'server'.");
        //    Console.WriteLine("Om een client te starten typ dan 'client'.");
        //    String result = Console.ReadLine();
        //    if (result == "server")
        //        Server = new ServerGanzenbord();
        //    else if (result == "client")
        //        Client = new ClientGanzenbord();

        //}

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Bord());
        }
    }
}
