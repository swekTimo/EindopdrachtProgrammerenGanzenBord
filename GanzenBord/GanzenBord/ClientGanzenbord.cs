﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GanzenBord
{
    class ClientGanzenbord
    {
        TcpClient client;

        Player player;

        public void makeConnectionWithTheServer()
        {
            client = new TcpClient(GetLocalIPAddress(), 6666);
        }

        public String getMessage()
        {
            return ReadMessage(client);
        }

        public void sendMessage(String message)
        {
            WriteMessage(client, message);
        }

        public void DitWasDeConstructorMaarDieGaanWeWegdoen(Bord bord)
        {
            //this.bord = bord;
            Console.WriteLine("Hij komt nu in de client code");
            string message;
            client = new TcpClient(GetLocalIPAddress(), 6666);
            Console.WriteLine("Hij zou nu verbinding moeten hebben gemaakt met de server");
            message = ReadMessage(client);
            Console.Write("de message van de server:  ");
            Console.WriteLine(message);
            if (message == 1.ToString())
            {
                //Hij moet dit doen met een Invoke, maar ik krijg het niet werken
                bord.Focus();
                //bord.Show();
                bord.Refresh();
                //Bord.howManyPlayersLabel.BringToFront();
            }

            //int.TryParse(message, out int value);



            message = ReadMessage(client);
            int RankValue;
            int.TryParse(message, out RankValue);
            //player = new Player(value, RankValue);

            int PlayerField = 0;
            bool done = false;
            while (!done)
            {
                message = ReadMessage(client);
                if (message != "player 1 has won!"
                    && message != "player 2 has won!"
                    && message != "player 3 has won!"
                    && message != "player 4 has won!")
                {

                    player.MoveGoose();
                    if (PlayerField != player.Field)
                        message = "SameTile";
                    else
                        message = player.Field.ToString();
                    PlayerField = player.Field;


                    message = ReadMessage(client);
                    int.TryParse(message, out RankValue);
                    player.Ranking = RankValue;


                    if (player.hasWon)
                        message = "endGame";
                    else
                        message = "KeepPlaying";
                    WriteMessage(client, message);


                    message = ReadMessage(client);
                    if (message == "bye")
                        done = true;
                }
                client.Close();
            }
        }








        private string ReadMessage(TcpClient client)
        {
            StreamReader streamReader = new StreamReader(client.GetStream(), Encoding.UTF8);
            return streamReader.ReadLine();
        }

        private void WriteMessage(TcpClient client, string message)
        {
            StreamWriter streamWriter = new StreamWriter(client.GetStream(), Encoding.UTF8);
            streamWriter.WriteLine(message);
            streamWriter.Flush();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
