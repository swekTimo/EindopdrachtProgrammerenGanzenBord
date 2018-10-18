using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class ServerGanzenbord
    {
        private int playerCount = 0;
        private int howMuchPlayersDoesClientWant = 4;
        private TcpClient client1 = null;
        private TcpClient client2 = null;
        private TcpClient client3 = null;
        private TcpClient client4 = null;
        private int positionClient1 = 0;
        private int positionClient2 = 0;
        private int positionClient3 = 0;
        private int positionClient4 = 0;
        private String gameWinner = null;

        public ServerGanzenbord()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 6666);
            listener.Start();

            while (playerCount < howMuchPlayersDoesClientWant)
            {
                TcpClient client = listener.AcceptTcpClient();
                playerCount++;
                if (playerCount == 1)
                {
                    client1 = client;
                    Thread thread = new Thread(HandleClient);
                    thread.Start(client1);
                }
                if (playerCount == 2)
                { 
                    client2 = client;
                Thread thread = new Thread(HandleClient);
                thread.Start(client2);
            }
            if (playerCount == 3)
                { 
                    client3 = client;
                Thread thread = new Thread(HandleClient);
                thread.Start(client3);
            }
            if (playerCount == 4)
                { 
                    client4 = client;
                Thread thread = new Thread(HandleClient);
                thread.Start(client4);
            }


        }
        }

        public void HandleClient(object obj)
        {

            TcpClient client = obj as TcpClient;
            WriteInteger(client1, playerCount);

            if (playerCount == 1)
            {
                howMuchPlayersDoesClientWant = Convert.ToInt32(ReadMessage(client1));
            }

            bool waitingForAllThePlayers = true;
            while (waitingForAllThePlayers)
            {
                //if (playerCount == howMuchPlayersDoesClientWant)
                if(howMuchPlayersDoesClientWant == 49)
                {
                    writeClientsStartGame();
                    waitingForAllThePlayers = false;
                }
            }

            bool gameAlive = true;
            while (gameAlive)
            {
                turnPlayer1();
                positionClient1 = ReadInteger(client1);

                //WriteInteger(client2, positionClient1);
                if (playerCount == 3)
                    WriteInteger(client3, positionClient1);
                if (playerCount == 4)
                {
                    WriteInteger(client3, positionClient1);
                    WriteInteger(client4, positionClient1);
                }

                checkForWinner();



                turnPlayer2();
                positionClient2 = ReadInteger(client2);

                WriteInteger(client1, positionClient2);
                if (playerCount == 3)
                    WriteInteger(client3, positionClient2);
                if (playerCount == 4)
                {
                    WriteInteger(client3, positionClient2);
                    WriteInteger(client4, positionClient2);
                }

                checkForWinner();




                if (playerCount == 3)
                {
                    turnPlayer3();
                    positionClient3 = ReadInteger(client3);

                    WriteInteger(client1, positionClient3);
                    WriteInteger(client2, positionClient3);
                }

                checkForWinner();




                if (playerCount == 4)
                {
                    turnPlayer3();
                    positionClient3 = ReadInteger(client3);

                    WriteInteger(client1, positionClient3);
                    WriteInteger(client2, positionClient3);
                    WriteInteger(client4, positionClient3);
                    

                    turnPlayer4();
                    positionClient4 = ReadInteger(client4);

                    WriteInteger(client1, positionClient4);
                    WriteInteger(client2, positionClient4);
                    WriteInteger(client3, positionClient4);
                }
            }
        }

        public bool checkForWinner()
        {
            bool winner = false;
            if (positionClient1 >= 63)
            {
                winner = true;
                gameWinner = "client1";
            }
            if (positionClient2 >= 63)
            {
                winner = true;
                gameWinner = "client2";
            }
            if (positionClient3 >= 63)
            {
                winner = true;
                gameWinner = "client3";
            }
            if (positionClient4 >= 63)
            {
                winner = true;
                gameWinner = "client4";
            }
            return winner;
        }

        public void writeClientsStartGame()
        {
            WriteMessage(client1, "startGame");
            //WriteMessage(client2, "startGame");
            //WriteMessage(client3, "startGame");
            //WriteMessage(client4, "startGame");
        }

        public void turnPlayer1()
            {
                WriteMessage(client1, "yourTurn");
                //WriteMessage(client2, "turnPlayer1");
                if (playerCount == 3)
                    WriteMessage(client3, "turnPlayer1");
                if (playerCount == 4)
                {
                    WriteMessage(client3, "turnPlayer1");
                    WriteMessage(client4, "turnPlayer1");
                }
            }

        public void turnPlayer2()
        {
            WriteMessage(client1, "turnPlayer2");
            WriteMessage(client2, "yourTurn");
            if (playerCount == 3)
                WriteMessage(client3, "turnPlayer2");
            if (playerCount == 4)
            {
                WriteMessage(client3, "turnPlayer2");
                WriteMessage(client4, "turnPlayer2");
            }
        }

        public void turnPlayer3()
        {
            WriteMessage(client1, "turnPlayer3");
            WriteMessage(client2, "turnPlayer3");
            if (playerCount == 3)
                WriteMessage(client3, "yourTurn");
            if (playerCount == 4)
            {
                WriteMessage(client3, "turnPlayer3");
                WriteMessage(client4, "turnPlayer3");
            }
        }

        public void turnPlayer4()
        {
            WriteMessage(client1, "turnPlayer4");
            WriteMessage(client2, "turnPlayer4");
            if (playerCount == 3)
                WriteMessage(client3, "turnPlayer4");
            if (playerCount == 4)
            {
                WriteMessage(client3, "turnPlayer4");
                WriteMessage(client4, "yourTurn");
            }
        }





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



        //client.Close();



        private PlayerRanking GetPlayerRanking(string playerID)
        {
            PlayerRanking ranking = new PlayerRanking();
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), $"{playerID}.txt");
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

        private void WriteInteger(TcpClient client, int message)
        {
            StreamWriter streamWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }

        private int ReadInteger(TcpClient client)
        {
            StreamReader streamReader = new StreamReader(client.GetStream(), Encoding.UTF8);
            return streamReader.Read();
        }


    }
}
