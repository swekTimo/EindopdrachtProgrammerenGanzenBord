using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GanzenBord
{
    class ServerGanzenbord
    {
        private int playerCount = 0;


        //luistert of er clients verbinding proberen te maken en start voor elk verbonden client een aparte thread
        public ServerGanzenbord()
        {
            //begint te luisteren of er IP adressen verbinding proberen te maken
            TcpListener listener = new TcpListener(IPAddress.Any, 6666);//moeten een poort afspreken
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Thread thread = new Thread(HandleClient);
                thread.Start(client);
                Console.WriteLine("Een client heeft verbinding gemaakt en er is een thread voor gestart");
            }
        }

        public void HandleClient(object obj)
        {
            playerCount++;
            TcpClient client = obj as TcpClient;
            WriteMessage(client, playerCount.ToString());

        }

        private void WriteMessage(TcpClient client, string message)
        {
            StreamWriter streamWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }

        private string ReadMessage(TcpClient client)
        {
            StreamReader streamReader = new StreamReader(client.GetStream(), Encoding.UTF8);
            return streamReader.ReadLine();
        }
    }
}
