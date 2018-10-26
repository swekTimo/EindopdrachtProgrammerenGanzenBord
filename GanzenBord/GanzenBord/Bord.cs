using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GanzenBord
{
    public partial class Bord : Form
    {
        private List<PictureBox> pictures;
        ClientGanzenbord client;
        private int playerNumber = 0;
        private int currentPosition = 0;
        private GameLogics gameLogics;
        private PlayerRanking ranking;

        private string userName;
        private string winnerUserName;

        private int currentPositionPlayer1 = 0;
        private int currentPositionPlayer2 = 0;
        private int currentPositionPlayer3 = 0;
        private int currentPositionPlayer4 = 0;

        private string playerColour = "red";

        private bool DiceRolled = false;
        private bool waitForDice = false;
        bool PlayNextTrun = true;
        bool Wait = false;
        bool firstTurn = true;

        public Bord()
        {
            Console.WriteLine("CLIENT");
            Console.WriteLine("");
            InitializeComponent();
            gameLogics = GameLogics.GetInstance();
            ranking = new PlayerRanking();
            pictures = new List<PictureBox>();
            generatePictureList();
            firstTurn = true;
            RulesBox.Visible = false;
            howManyPlayersLabel.Visible = false;
            playerNameLabel.Visible = false;
            twoPlayerGameButton.Visible = false;
            threePlayerGameButton.Visible = false;
            fourPlayerGameButton.Visible = false;
            waitForAllPlayersLabel.Visible = false;
            rulesButton.Visible = false;
            beurtInfoLabel.Visible = false;
            startGameButton.Visible = false;

            SendAllPicturesToBack();


            client = new ClientGanzenbord();
            client.makeConnectionWithTheServer();
        }

        private void SendAllPicturesToBack()
        {
            foreach (PictureBox image in pictures)
            {
                image.SendToBack();
            }
            PictureBox picture = pictures.ElementAt(0);
            picture.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood");
            picture.BringToFront();
        }

        private void generatePictureList()
        {
            pictures.Add(startPictureBox);
            pictures.Add(onePictureBox);
            pictures.Add(twoPictureBox);
            pictures.Add(threePictureBox);
            pictures.Add(fourPictureBox);
            pictures.Add(fivePictureBox);
            pictures.Add(sixPictureBox);
            pictures.Add(sevenPictureBox);
            pictures.Add(eightPictureBox);
            pictures.Add(ninePictureBox);
            pictures.Add(tenPictureBox);
            pictures.Add(elevenPictureBox);
            pictures.Add(twelvePictureBox);
            pictures.Add(thirteenPictureBox);
            pictures.Add(fourteenPictureBox);
            pictures.Add(fifteenPictureBox);
            pictures.Add(sixteenPictureBox);
            pictures.Add(seventeenPictureBox);
            pictures.Add(eightteenPictureBox);
            pictures.Add(nineteenPictureBox);
            pictures.Add(twentyPictureBox);
            pictures.Add(twentyonePictureBox);
            pictures.Add(twentytwoPictureBox);
            pictures.Add(twentrythreePictureBox);
            pictures.Add(twentyfourPictureBox);
            pictures.Add(twentyfivePictureBox);
            pictures.Add(twentysixPictureBox);
            pictures.Add(twentysevenPictureBox);
            pictures.Add(twentyeightPictureBox);
            pictures.Add(twentyninePictureBox);
            pictures.Add(thirtyPictureBox);
            pictures.Add(thirtyonePictureBox);
            pictures.Add(thirtytwoPictureBox);
            pictures.Add(thirtythreePictureBox);
            pictures.Add(thirtyfourPictureBox);
            pictures.Add(thirtyfivePictureBox);
            pictures.Add(thirtysixPictureBox);
            pictures.Add(thirtysevenPictureBox);
            pictures.Add(thirtyeightPictureBox);
            pictures.Add(thirtyninePictureBox);
            pictures.Add(fourtyPictureBox);
            pictures.Add(fourtyonePictureBox);
            pictures.Add(fourtytwoPictureBox);
            pictures.Add(fourtythreePictureBox);
            pictures.Add(fourtyfourPictureBox);
            pictures.Add(fourtyfivePictureBox);
            pictures.Add(fourtysixPictureBox);
            pictures.Add(fourtysevenPictureBox);
            pictures.Add(fourtyeightPictureBox);
            pictures.Add(fourtyninePictureBox);
            pictures.Add(fiftyPictureBox);
            pictures.Add(fiftyonePictureBox);
            pictures.Add(fiftytwoPictureBox);
            pictures.Add(fiftythreePictureBox);
            pictures.Add(fiftyfourPictureBox);
            pictures.Add(fiftyfivePictureBox);
            pictures.Add(fiftysixPictureBox);
            pictures.Add(fiftysevenPictureBox);
            pictures.Add(fiftyeightPictureBox);
            pictures.Add(fiftyninePictureBox);
            pictures.Add(sixtyPictureBox);
            pictures.Add(sixtyonePictureBox);
            pictures.Add(sixtytwoPictureBox);
            pictures.Add(sixtythreePictureBox);
        }


        private void startGameButton_Click(object sender, EventArgs e)
        {
            playerNumber = Convert.ToInt32(client.ReadMessage());
            Console.WriteLine("leest hier van de server: welk nummer client we zijn: client nummer: ");
            Console.Write(playerNumber);
            Console.WriteLine(" ");
            playerNameLabel.Text = "Player " + playerNumber + "  :  " + userName;
            playerNameLabel.Visible = true;
            startGameButton.Visible = false;
            howManyPlayersLabel.Visible = true;
            if (playerNumber == 1)
            {
                twoPlayerGameButton.Visible = true;
                threePlayerGameButton.Visible = true;
                fourPlayerGameButton.Visible = true;
                playerColour = "red";
            }
            else
            {
                if (playerNumber == 2) { playerColour = "green"; }
                if (playerNumber == 3) { playerColour = "blue"; }
                if (playerNumber == 4) { playerColour = "yellow"; }
                waitForAllPlayersLabel.Visible = true;
                this.Update();
                waitForGameToStart();
            }

        }

        private void twoPlayerGameButton_Click(object sender, EventArgs e)
        {
            client.WriteMessage("2");
            howManyPlayersLabel.Visible = false;
            twoPlayerGameButton.Visible = false;
            threePlayerGameButton.Visible = false;
            fourPlayerGameButton.Visible = false;
            waitForAllPlayersLabel.Visible = true;
            this.Update();
            waitForGameToStart();
        }

        private void threePlayerGameButton_Click(object sender, EventArgs e)
        {
            client.WriteMessage("3");
            howManyPlayersLabel.Visible = false;
            twoPlayerGameButton.Visible = false;
            threePlayerGameButton.Visible = false;
            fourPlayerGameButton.Visible = false;
            waitForAllPlayersLabel.Visible = true;
            this.Update();
            waitForGameToStart();
        }

        private void fourPlayerGameButton_Click(object sender, EventArgs e)
        {
            //dit moet natuurlijk 4 zijn maar om te testen staat hier 1
            client.WriteMessage("1");
            Console.WriteLine("verstuurd naar de server met hoeveel spelers we willen spelen: ");
            Console.WriteLine("1");
            Console.WriteLine(" ");
            howManyPlayersLabel.Visible = false;
            twoPlayerGameButton.Visible = false;
            threePlayerGameButton.Visible = false;
            fourPlayerGameButton.Visible = false;
            waitForAllPlayersLabel.Visible = true;
            Update();
            waitForGameToStart();

        }

        private void waitForGameToStart()
        {
            string message = client.ReadMessage();
            Console.WriteLine("leest hier van de server de message wat er kan gaan gebeuren: ");
            Console.WriteLine(message);
            Console.WriteLine(" ");
            if (message == "startGame")
            {
                rulesButton.Visible = true;
                waitForAllPlayersLabel.Visible = false;
            }
            this.Update();
            client.WriteMessage("gameCodeStarted");
            Console.WriteLine("schrijf hier naar de server dat de game code is gestart: ");
            Console.WriteLine("gameCodeStarted");
            Console.WriteLine(" ");
            game();
        }

        private void game()
        {
            string message = client.ReadMessage();
            Console.WriteLine("leest hier van de server of het onze beurt is of van een andere client: ");
            Console.WriteLine(message);
            Console.WriteLine(" ");
            if (message == "yourTurn")
            {
                rollDiceButton.Enabled = true;
                beurtInfoLabel.Text = "Its your turn to throw";
                beurtInfoLabel.Visible = true;
            }
            else if (message == "turnPlayer1")
            {
                beurtInfoLabel.Text = "Player 1 is now throwing";
                int positionPlayer1 = Convert.ToInt32(client.ReadMessage());
                //move de rode gans naar het nummer wat door komt
                MoveGooseTile("red", currentPositionPlayer1, positionPlayer1);
                currentPositionPlayer1 = positionPlayer1;


                client.WriteMessage("DONE");
                //hij moet hier iets sturen anders doen de read/write het niet meer
            }
            else if (message == "turnPlayer2")
            {
                beurtInfoLabel.Text = "Player 2 is now throwing";
                int positionPlayer2 = Convert.ToInt32(client.ReadMessage());
                //move de groene gans naar het nummer wat door komt
                MoveGooseTile("green", currentPositionPlayer2, positionPlayer2);
                currentPositionPlayer2 = positionPlayer2;

                client.WriteMessage("DONE");
                //hij moet hier iets sturen anders doen de read/write het niet meer
            }
            else if (message == "turnPlayer3")
            {
                beurtInfoLabel.Text = "Player 3 is now throwing";
                int positionPlayer3 = Convert.ToInt32(client.ReadMessage());
                //move de blauwe gans naar het nummer wat doorkomt
                MoveGooseTile("blue", currentPositionPlayer3, positionPlayer3);
                currentPositionPlayer3 = positionPlayer3;

                client.WriteMessage("DONE");
                //hij moet hier iets sturen anders doen de read/write het niet meer
            }
            else if (message == "turnPlayer4")
            {
                beurtInfoLabel.Text = "Player 4 is now throwing";
                int positionPlayer4 = Convert.ToInt32(client.ReadMessage());
                //move de gele gans naar het nummer wat doorkomt
                MoveGooseTile("yellow", currentPositionPlayer4, positionPlayer4);
                currentPositionPlayer4 = positionPlayer4;

                client.WriteMessage("DONE");
                //hij moet hier iets sturen anders doen de read/write het niet meer
            }
            else if (message == "Winnerclient1")
            {
                winnerUserName = client.ReadMessage();
                beurtInfoLabel.Text = "Player 1: " + winnerUserName + " has won the game";
            }
            else if (message == "Winnerclient2")
            {
                winnerUserName = client.ReadMessage();
                beurtInfoLabel.Text = "Player 2: " + winnerUserName + " has won the game";
            }
            else if (message == "Winnerclient3")
            {
                winnerUserName = client.ReadMessage();
                beurtInfoLabel.Text = "Player 3:"  + winnerUserName + " has won the game";
            }
            else if (message == "Winnerclient4")
            {
                winnerUserName = client.ReadMessage();
                beurtInfoLabel.Text = "Player 4: " + winnerUserName + " has won the game";
            }
        }

        private void rulesButton_MouseHover(object sender, EventArgs e)
        {
            RulesBox.Visible = true;
            RulesBox.BringToFront();
        }

        private void rulesButton_MouseLeave(object sender, EventArgs e)
        {
            RulesBox.Visible = false;
        }

        public void moveGoosePosition(string duckColour, int currentTile, int toTile)
        {

            MoveGooseTile(duckColour, currentTile, toTile);
            currentTile = toTile;
        }

        public void MoveGooseTile(string DuckColour, int previousDuckTile, int nextDuckTile)
        {
            ChangeDuckFromTile(DuckColour, previousDuckTile);

            ChangeDuckToTile(DuckColour, nextDuckTile);

        }

        private void ChangeDuckFromTile(string duckColour, int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw")
                || duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel")
                || duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen")
                || duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood"))
            {
                duckTile.Image = null;
                duckTile.SendToBack();
            }
            else if (duckColour == "red")
                RedDuckFromTile(duckTileID);
            else if (duckColour == "blue")
                BlueDuckFromTile(duckTileID);
            else if (duckColour == "yellow")
                YellowDuckFromTile(duckTileID);
            else if (duckColour == "green")
                GreenDuckFromTile(duckTileID);

        }

        private void ChangeDuckToTile(string duckColour, int duckTileID)
        {
            if (duckColour == "red")
                RedDuckToTile(duckTileID);
            else if (duckColour == "blue")
                BlueDuckToTile(duckTileID);
            else if (duckColour == "yellow")
                YellowDuckToTile(duckTileID);
            else if (duckColour == "green")
                GreenDuckToTile(duckTileID);
        }

        private void RedDuckFromTile(int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGanGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeel"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw");
        }

        private void RedDuckToTile(int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGanGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeel");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnBlauw");

            else if (duckTile.Image == null)
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood");

            duckTile.BringToFront();
        }

        private void BlueDuckFromTile(int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroen");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeel");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroen");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen");
        }

        private void BlueDuckToTile(int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroenEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeel"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroenEnBlauw");

            else if (duckTile.Image == null)
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw");

            duckTile.BringToFront();
        }

        private void YellowDuckFromTile(int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroenEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeel"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood");
        }

        private void YellowDuckToTile(int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroenEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeel");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroen");

            else if (duckTile.Image == null)
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel");

            duckTile.BringToFront();
        }

        private void GreenDuckFromTile(int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeel");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroenEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroen"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood");
        }

        private void GreenDuckToTile(int duckTileID)
        {
            PictureBox duckTile = pictures.ElementAt(duckTileID);
            if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroenEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeel"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGeelEnGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroenEnBlauw");

            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroenEnBlauw");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeelEnGroen");
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood"))
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRoodEnGroen");

            else if (duckTile.Image == null)
                duckTile.Image = (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen");

            duckTile.BringToFront();
        }

        private void rollDiceButton_Click(object sender, EventArgs e)
        {
            rollDiceButton.Enabled = false;
            int thrownDiceNumber;
            gameLogics.RollDice(out thrownDiceNumber);
            diceButton.Text = thrownDiceNumber.ToString();
            Console.WriteLine("dice rolled");

            if (Wait == true)
            {
                if (!isSameTile())
                    PlayNextTrun = false;
                else Wait = false;
            }

            if (PlayNextTrun)
            {
                int newPosition = currentPosition + thrownDiceNumber;
                if (newPosition > 63)
                {
                    newPosition = 63 - (thrownDiceNumber - (63 - currentPosition));
                }
                moveGoosePosition(playerColour, currentPosition, newPosition);
                currentPosition = newPosition;
                specialFieldAction();



                ranking.AddPoints(currentPosition);
            }
            else
                PlayNextTrun = true;

            client.WriteMessage(ranking.Ranking.ToString());

            Console.WriteLine("stuur hier naar de server op welke positie de Client staat");
            Console.WriteLine(currentPosition.ToString());
            Console.WriteLine("");
            client.WriteMessage(currentPosition.ToString());
            game();
        }

        private bool isSameTile()
        {
            if (playerNumber == 1
                && currentPosition != currentPositionPlayer2
                && currentPosition != currentPositionPlayer3
                && currentPosition != currentPositionPlayer4)
                return false;
            else if (playerNumber == 2
                && currentPosition != currentPositionPlayer1
                && currentPosition != currentPositionPlayer3
                && currentPosition != currentPositionPlayer4)
                return false;
            else if (playerNumber == 3
                && currentPosition != currentPositionPlayer1
                && currentPosition != currentPositionPlayer2
                && currentPosition != currentPositionPlayer4)
                return false;
            else if (playerNumber == 4
                && currentPosition != currentPositionPlayer1
                && currentPosition != currentPositionPlayer2
                && currentPosition != currentPositionPlayer3)
                return false;
            else
                return true;
        }

        private void specialFieldAction()
        {
            Tuple<bool, SpecialField> tuple = gameLogics.IsSpecialField(currentPosition);
            if (tuple.Item1)
            {
                SpecialField field = tuple.Item2;
                switch (field.Command)
                {
                    case SpecialField.CommandOptions.GoTO:
                        moveGoosePosition(playerColour, currentPosition, field.FieldNumber);
                        break;
                    case SpecialField.CommandOptions.SkipTurn:
                        PlayNextTrun = false;
                        break;
                    case SpecialField.CommandOptions.Wait:
                        Wait = true;
                        break;
                    case SpecialField.CommandOptions.End:
                        break;
                }
            }
        }

        private void userNameButton_Click(object sender, EventArgs e)
        {
            userName = userNameTextBox.Text;
            userNameLabel.Visible = false;
            userNameButton.Visible = false;
            userNameTextBox.Visible = false;
            startGameButton.Visible = true;
            client.WriteMessage(userName);
        }
    }
}
