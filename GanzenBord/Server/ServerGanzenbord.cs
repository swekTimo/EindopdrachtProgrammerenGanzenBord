﻿using System;
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
        private string userNameClient1;
        private string userNameClient2;
        private string userNameClient3;
        private string userNameClient4;
        private string gameWinner;
        private string gameWinnerUsername;

        public ServerGanzenbord()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 6666);
            listener.Start();
            Console.WriteLine("This is the SERVER console!!!" + Environment.NewLine);
            while (playerCount < howMuchPlayersDoesClientWant)
            { 
                TcpClient client = listener.AcceptTcpClient();
                playerCount++;


                if (playerCount == 1)
                {
                    client1 = client;
                    Thread thread = new Thread(HandleClient);
                    thread.Start(client1);
                    Console.WriteLine("Started thread for client1");
                }
                if (playerCount == 2)
                {
                    client2 = client;
                    Thread thread = new Thread(HandleClient);
                    thread.Start(client2);
                    Console.WriteLine("Started thread for client2");
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
            if (obj.Equals(client1))
            {
                Console.WriteLine("Now in the handleclient from client1");
                userNameClient1 = ReadMessage(client1);
                Console.WriteLine("Username From client1: " + userNameClient1);
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), userNameClient1 + ".txt");
                if (!File.Exists(path))
                {
                    FileStream myFile = File.Create(path);
                    myFile.Close();
                }
                WriteMessage(client1, "1");
                Console.WriteLine("Message send to client1: 1" );
                Console.WriteLine("Waiting for player1 to answer, with how much players he wants to play");
                howMuchPlayersDoesClientWant = Convert.ToInt32(ReadMessage(client1));
                Console.WriteLine("Hij heeft nu binnen van player1 dat hij met " + howMuchPlayersDoesClientWant + " wilt spelen");
            }
            if (obj.Equals(client2))
            {
                Console.WriteLine("Now in the handleclient from client2");
                userNameClient2 = ReadMessage(client2);
                Console.WriteLine("Username From client2: " + userNameClient2);
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), userNameClient2 + ".txt");
                if (!File.Exists(path))
                {
                    FileStream myFile = File.Create(path);
                    myFile.Close();
                }
                WriteMessage(client1, "2");
                Console.WriteLine("Message send to client1: 1");
            }
            if (obj.Equals(client3))
            { 
                userNameClient3 = ReadMessage(client3);
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), userNameClient3 + ".txt");
                if (!File.Exists(path))
                {
                    FileStream myFile = File.Create(path);
                    myFile.Close();
                }
                WriteMessage(client1, "3");
            }
            if (obj.Equals(client4))
            {
                userNameClient4 = ReadMessage(client4);
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), userNameClient4 + ".txt");
                if (!File.Exists(path))
                {
                    FileStream myFile = File.Create(path);
                    myFile.Close();
                }
                WriteMessage(client1, "4");
            }


            bool waitingForAllThePlayers = true;
            while (waitingForAllThePlayers)
            {
                if (playerCount == howMuchPlayersDoesClientWant)
                {
                    writeClientsStartGame();
                    waitingForAllThePlayers = false;
                }
            }
            string isGameStarted = ReadMessage(client1);
            if (isGameStarted == "gameCodeStarted")
            {
                Console.WriteLine("hij heeft nu binnen gehad dat de game is gestart en gaat de while loop in:");
                Console.WriteLine("");
            }


            bool gameAlive = true;
            while (gameAlive)
            {
                turnPlayer1();
                if (checkForWinner() == true)
                {
                    writeAllClients(gameWinner);
                    writeAllClients(gameWinnerUsername);
                }

                //turnPlayer2();
                //if (checkForWinner() == true) 
                //{ 
                //writeAllClients(gameWinner);
                //writeAllClients(gameWinnerUsername);
                //}

                if (client3 != null)
                {
                    turnPlayer3();
                    if (checkForWinner() == true)
                    {
                        writeAllClients(gameWinner);
                        writeAllClients(gameWinnerUsername);
                    }
                }

                if (client4 != null)
                {
                    turnPlayer4();
                    if (checkForWinner() == true)
                    {
                        writeAllClients(gameWinner);
                        writeAllClients(gameWinnerUsername);
                    }
                }
            }
        }

        public void writeAllClients(string message)
        {
            WriteMessage(client1, message);
            //WriteMessage(client2, message);
            if (client3 != null) WriteMessage(client3, message);
            if (client4 != null) WriteMessage(client4, message);
        }

        public bool checkForWinner()
        {
            bool winner = false;
            if (positionClient1 == 63)
            {
                winner = true;
                gameWinner = "Winnerclient1";
                gameWinnerUsername = userNameClient1;
            }
            if (positionClient2 == 63)
            {
                winner = true;
                gameWinner = "Winnerclient2";
                gameWinnerUsername = userNameClient2;
            }
            if (positionClient3 == 63)
            {
                winner = true;
                gameWinner = "Winnerclient3";
                gameWinnerUsername = userNameClient3;
            }
            if (positionClient4 == 63)
            {
                winner = true;
                gameWinner = "Winnerclient4";
                gameWinnerUsername = userNameClient4;
            }
            return winner;
        }

        public void writeClientsStartGame()
        {
            WriteMessage(client1, "startGame");
            Console.WriteLine("stuurt hier naar de client1 dat de game kan beginnen : ");
            Console.WriteLine("startGame");
            Console.WriteLine(" ");
            //WriteMessage(client2, "startGame");
            if (client3 != null) WriteMessage(client3, "startGame");
            if (client4 != null) WriteMessage(client4, "startGame");
        }

        public void turnPlayer1()
            {
            WriteMessage(client1, "yourTurn");
            Console.WriteLine("stuurt hier naar de client dat het zijn beurt is of van andere client: ");
            Console.WriteLine("yourturn");
            Console.WriteLine(" ");
            //WriteMessage(client2, "turnPlayer1");
            if (client3 != null) { WriteMessage(client3, "turnPlayer1"); }
            if (client4 != null) { WriteMessage(client4, "turnPlayer1"); }

            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), userNameClient1 + ".txt");
            string data = ReadMessage(client1);
            File.WriteAllText(path, data);

            positionClient1 = Convert.ToInt32(ReadMessage(client1));
            Console.WriteLine("leest hier van de client wat zijn positie is na het gooien: ");
            Console.WriteLine(positionClient1);
            Console.WriteLine(" ");

            //WriteMessage(client2, positionClient1.ToString());
            if (client3 != null) { WriteMessage(client3, positionClient1.ToString()); }
            if (client4 != null) { WriteMessage(client4, positionClient1.ToString()); }


            //if (ReadMessage(client2) == "DONE") { Console.WriteLine("client2 heeft succesvol de positie van clinent 1 ontvangen"); }
            if (client3 != null)
                if (ReadMessage(client3) == "DONE") { Console.WriteLine("client3 heeft succesvol de positie van clinent 1 ontvangen"); }
            if (client4 != null)
                if (ReadMessage(client4) == "DONE") { Console.WriteLine("client4 heeft succesvol de positie van clinent 1 ontvangen"); }
        }

        public void turnPlayer2()
        {
            WriteMessage(client1, "turnPlayer2");
            WriteMessage(client2, "yourTurn");
            if (client3 != null) { WriteMessage(client3, "turnPlayer2"); }
            if (client4 != null) { WriteMessage(client4, "turnPlayer2"); }

            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), userNameClient2 + ".txt");
            string data = ReadMessage(client2);
            File.WriteAllText(path, data);

            positionClient2 = Convert.ToInt32(ReadMessage(client2));

            WriteMessage(client1, positionClient2.ToString());
            if (client3 != null) { WriteMessage(client3, positionClient1.ToString()); }
            if (client4 != null) { WriteMessage(client4, positionClient1.ToString()); }

            if (ReadMessage(client1) == "DONE") { Console.WriteLine("client1 heeft succesvol de positie van clinent 2 ontvangen"); }
            if (client3 != null)
                if (ReadMessage(client3) == "DONE") { Console.WriteLine("client3 heeft succesvol de positie van clinent 2 ontvangen"); }
            if (client4 != null)
                if (ReadMessage(client4) == "DONE") { Console.WriteLine("client4 heeft succesvol de positie van clinent 2 ontvangen"); }
        }

        public void turnPlayer3()
        {
            WriteMessage(client1, "turnPlayer3");
            WriteMessage(client2, "turnPlayer3");
            if (client3 != null) { WriteMessage(client3, "yourTurn"); }
            if (client4 != null) { WriteMessage(client4, "turnPlayer3"); }

            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), userNameClient3 + ".txt");
            string data = ReadMessage(client3);
            File.WriteAllText(path, data);

            positionClient3 = Convert.ToInt32(ReadMessage(client3));

            WriteMessage(client1, positionClient3.ToString());
            WriteMessage(client2, positionClient3.ToString());
            if (client4 != null) { WriteMessage(client4, positionClient3.ToString()); }

            if (ReadMessage(client1) == "DONE") { Console.WriteLine("client1 heeft succesvol de positie van clinent 3 ontvangen"); }
            if (ReadMessage(client2) == "DONE") { Console.WriteLine("client2 heeft succesvol de positie van clinent 3 ontvangen"); }
            if (client4 != null)
                if (ReadMessage(client4) == "DONE") { Console.WriteLine("client4 heeft succesvol de positie van clinent 3 ontvangen"); }
        }

        public void turnPlayer4()
        {
            WriteMessage(client1, "turnPlayer4");
            WriteMessage(client2, "turnPlayer4");
            if (client3 != null) { WriteMessage(client3, "turnPlayer4"); }
            if (client4 != null) { WriteMessage(client4, "yourTurn"); }

            var path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), userNameClient4 + ".txt");
            string data = ReadMessage(client4);
            File.WriteAllText(path, data);

            positionClient4 = Convert.ToInt32(ReadMessage(client4));

            WriteMessage(client1, positionClient4.ToString());
            WriteMessage(client2, positionClient4.ToString());
            WriteMessage(client3, positionClient4.ToString());

            if (ReadMessage(client1) == "DONE") { Console.WriteLine("client1 heeft succesvol de positie van clinent 4 ontvangen"); }
            if (ReadMessage(client2) == "DONE") { Console.WriteLine("client2 heeft succesvol de positie van clinent 4 ontvangen"); }
            if (ReadMessage(client3) == "DONE") { Console.WriteLine("client3 heeft succesvol de positie van clinent 4 ontvangen"); }
        }

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

    }
}
