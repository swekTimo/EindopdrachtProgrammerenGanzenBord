using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GanzenBord
{
    public partial class Bord : Form
    {
        private List<PictureBox> pictures;
        ClientGanzenbord client;
        int playerNumber = 0;
        public Bord()
        {
            InitializeComponent();
            pictures = new List<PictureBox>();
            generatePictureList();
            RulesBox.Visible = false;
            howManyPlayersLabel.Visible = false;
            SendAllPicturesToBack();


            client = new ClientGanzenbord();
            client.makeConnectionWithTheServer();
 
        }

        private void SendAllPicturesToBack()
        {
            foreach(PictureBox picture in pictures)
            {
                picture.SendToBack();
            }
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
        
        private void button1_MouseHover(object sender, EventArgs e)
        {
            RulesBox.Visible = true;
        }

        private void startGameButton_Click(object sender, EventArgs e)
        {
            playerNumber = Convert.ToInt32(client.getMessage());
            if (playerNumber == 2)
            {
                howManyPlayersLabel.Visible = true; ;
            }
            startGameButton.Visible = false;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            RulesBox.Visible = false;
        }

        public void ShowHowManyPLayersLabel()
        {
            howManyPlayersLabel.BringToFront();
        }

        public void MoveGoose(string DuckColour, int previousDuckTile, int nextDuckTile)
        {
            //TODO: complete resources and change the naming of the ChangeDuck to and form tile classes
            PictureBox duckTile = pictures.ElementAt(previousDuckTile);
            ChangeDuckFromTile(DuckColour, duckTile);

            duckTile = pictures.ElementAt(nextDuckTile);

            ChangeDuckToTile(DuckColour, duckTile);
            duckTile.BringToFront();

        }

        private void ChangeDuckFromTile(string duckColour, PictureBox duckTile)
        {
            if (duckColour == "rood")
                RedDuckFromTile(duckTile);
            else if (duckColour == "Blauw")
                BlueDuckFromTile(duckTile);
            else if (duckColour == "geel")
                YellowDuckFromTile(duckTile);
            else if (duckColour == "groen")
                GreenDuckFromTile(duckTile);
            else if (duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansBlauw")
                || duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGeel")
                || duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansGroen")
                || duckTile.Image == (Image)Properties.Resources.ResourceManager.GetObject("ganzenBordGansRood"))
            {
                duckTile.Image = null;
                duckTile.SendToBack();
            }
        }

        private void ChangeDuckToTile(string duckColour, PictureBox duckTile)
        {
            if (duckColour == "rood")
                RedDuckToTile(duckTile);
            else if (duckColour == "Blauw")
                BlueDuckToTile(duckTile);
            else if (duckColour == "geel")
                YellowDuckToTile(duckTile);
            else if (duckColour == "groen")
                GreenDuckToTile(duckTile);
        }

        private void RedDuckFromTile(PictureBox duckTile)
        {
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

        private void RedDuckToTile(PictureBox duckTile)
        {
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
        }

        private void BlueDuckFromTile(PictureBox duckTile)
        {
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

        private void BlueDuckToTile(PictureBox duckTile)
        {
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
        }

        private void YellowDuckFromTile(PictureBox duckTile)
        {
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

        private void YellowDuckToTile(PictureBox duckTile)
        {
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
        }

        private void GreenDuckFromTile(PictureBox duckTile)
        {
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

        private void GreenDuckToTile(PictureBox duckTile)
        {
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
        }
    }
}
