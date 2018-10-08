using System;
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
        Player player;
        public ClientGanzenbord()
        {
            string message;
            TcpClient client = new TcpClient(GetLocalIPAddress(), 6666);

            message = ReadMessage(client);
            if (message == 1.ToString())
            {
                Bord.howManyPlayersLabel.BringToFront();
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
