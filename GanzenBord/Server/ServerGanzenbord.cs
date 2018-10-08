using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class ServerGanzenbord
    {
        private PlayerRanking playerRanking;

        private int playerCount = 0;
        private int howMuchPlayersDoesClientWant = 4;


        //luistert of er clients verbinding proberen te maken en start voor elk verbonden client een aparte thread
        public ServerGanzenbord()
        {

            //begint te luisteren of er IP adressen verbinding proberen te maken
            TcpListener listener = new TcpListener(IPAddress.Any, 6666);//moeten een poort afspreken
            listener.Start();

            // er moet dan aan player 1 nog gevraagd worden hoeveel spelers hij wilt in zijn spel, en dan moet die variable aangepast worden
            //hij moet wel op 4 beginnen want dat is het max aantal spelers
            while (playerCount < howMuchPlayersDoesClientWant)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine($"Accepted client at {DateTime.Now}");

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
            //playerRanking = GetPlayerRanking("player{0}", playerCount);
            //WriteMessage(client, $"{playerRanking.Ranking}");

            //hier moet nog een loop komen waar die in blijft
            // die loop houd bij hoeveel spelers er geconnect zijn
            //in de client gebeurd nog niks, totdat de server doorstuurt
            //dat de game kan beginnen.
            //dus de client moet wachten op een teken van de server dat het spel kan starten
            //in de client kun je dan groot in beeld zetten:
            //WACHTEN OP SPELERS...


            //hier begint de game, er moet dus gekeken worden wie er aan de beurt is
            // en dan struren dat diegene mag gooien
            //hij krijgt terug van de client hoeveel die gegooid heeft.
            //server verplaatst het aantal ogen, en stuurt naar alle clients
            //de kleur en hoeveel die voorruit is gegaan, dit moet dan dus naar alle clients gedaan worden
            //in de server moet bijgehouden worden welke kleur iedere client is
            //we laten ze geen kleur kiezen, word te ingewikkeld, gewoon client 1 is rood, client 2 geel etc
            //dan moet er nog een eindconditie komen, dus als iemand op 63 komt dat de game dan eindigd
            //63 of hoger natuurlijk je kan ook over 63 gooien, maar dan win je
            //we moeten nog ff beslissen of op welk valkje de client staat in de server of client bijgehouden word
            bool done = false;
            string message;
            string status = "Keep Playing";
            int tile = 0;
            while (!done)
            {
                WriteMessage(client, status);

                message = ReadMessage(client);
                if (message != "SameTile")
                {
                    int.TryParse(message, out tile);
                    playerRanking.AddPoints(tile);
                }

                WriteMessage(client, playerRanking.Ranking.ToString());

                message = ReadMessage(client);
                if (message == "endGame")
                {
                    done = true;
                    status = "player 1 has won!";
                }
                WriteMessage(client, "notDone");

            }



            WriteMessage(client, "bye");
            client.Close();

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
                //ranking = JsonConvert.DeserializeObject<PlayerRanking>(RankPlayer);

            }
            return ranking;
        }
    }
}
