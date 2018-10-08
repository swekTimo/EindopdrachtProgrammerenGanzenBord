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
using Newtonsoft.Json;


namespace GanzenBord
{
    class ServerGanzenbord
    {
        private PlayerRanking player1Ranking;
        private PlayerRanking Player2Ranking;
        private PlayerRanking player3Ranking;
        private PlayerRanking Player4Ranking;

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
            player1Ranking = GetPlayerRanking("player1");
            Player2Ranking = GetPlayerRanking("player2");
            player3Ranking = GetPlayerRanking("player3");
            Player4Ranking = GetPlayerRanking("player4");

            bool done = false;
            string message;
            int tile;
            while (!done)
            {
                message = ReadMessage(client);
                int.TryParse(message, out tile);
                player1Ranking.AddPoints(tile);
                if (ShitHappens)
                    done = true;
            }

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

        private PlayerRanking GetPlayerRanking(string playerID)
        {
            PlayerRanking ranking = new PlayerRanking();
            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), $"{playerID}.txt");
            if (!File.Exists(path))
            {
                Console.WriteLine("Er is nog geen File gevonden waarin de playerRank staat, dus deze word aangemaakt!");
                var myFile = File.Create(path);
                myFile.Close();
            }
            else if (File.Exists(path))
            {
                Console.WriteLine("Er is een bestand gevonden met bestaande player rank");
                string RankPlayer = File.ReadAllText(path);
                ranking = JsonConvert.DeserializeObject<PlayerRanking>(RankPlayer);
                
            }
            return ranking;
        }
    }
}
