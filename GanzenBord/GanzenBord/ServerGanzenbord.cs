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
                TcpClient client1 = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted client at {DateTime.Now}");
                TcpClient client2 = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted client at {DateTime.Now}");
                TcpClient client3 = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted client at {DateTime.Now}");
                TcpClient client4 = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted client at {DateTime.Now}");

                Thread thread = new Thread(unused => HandleClient(client1, client2, client3, client4));
                thread.Start();
                Console.WriteLine("Een client heeft verbinding gemaakt en er is een thread voor gestart");
            }
        }

        public void HandleClient(object obj, object obj2, object obj3, object obj4)
        {
            playerCount++;
            TcpClient client1 = obj as TcpClient;
            WriteMessage(client1, playerCount.ToString());
            player1Ranking = GetPlayerRanking("player1");
            WriteMessage(client1, $"{player1Ranking.Ranking}");

            playerCount++;
            TcpClient client2 = obj2 as TcpClient;
            WriteMessage(client2, playerCount.ToString());
            Player2Ranking = GetPlayerRanking("player2");
            WriteMessage(client2, $"{Player2Ranking.Ranking}");

            playerCount++;
            TcpClient client3 = obj3 as TcpClient;
            WriteMessage(client3, playerCount.ToString());
            player3Ranking = GetPlayerRanking("player3");
            WriteMessage(client3, $"{player3Ranking.Ranking}");

            playerCount++;
            TcpClient client4 = obj4 as TcpClient;
            WriteMessage(client4, playerCount.ToString());
            Player4Ranking = GetPlayerRanking("player4");
            WriteMessage(client4, $"{Player4Ranking.Ranking}");

            bool done = false;
            string message;
            int tile = 0;
            while (!done)
            {
                message = ReadMessage(client1);
                //if (message)
                int.TryParse(message, out tile);
                player1Ranking.AddPoints(tile);

                message = ReadMessage(client2);
                int.TryParse(message, out tile);
                Player2Ranking.AddPoints(tile);

                message = ReadMessage(client3);
                int.TryParse(message, out tile);
                player3Ranking.AddPoints(tile);

                message = ReadMessage(client4);
                int.TryParse(message, out tile);
                Player4Ranking.AddPoints(tile);
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
