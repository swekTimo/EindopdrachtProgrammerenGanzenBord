using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GanzenBord
{
    class PlayerRanking
    {
        public int Experiance { get; set; }
        public int Ranking { get; set; }
        public int PreviousRankingXP { get; set; }

        public PlayerRanking()
        {
            this.Experiance = 0;
            this.Ranking = 1;
            PreviousRankingXP = 10;
        }

        public void AddPoints(int tegelNummer)
        {
            Experiance = Experiance + tegelNummer;

            if (CanRankUp())
                RankUp();

        }

        private bool CanRankUp()
        {
            int NeededXP = (int)((PreviousRankingXP / 2) + (PreviousRankingXP * 1.25));
            if (NeededXP < Experiance)
                return true;
            else
                return false;
        }

        private void RankUp()
        {
            PreviousRankingXP = (int)((PreviousRankingXP / 2) + (PreviousRankingXP * 1.25));
            MessageBox.Show($"Your Duck Has Ranked UP, he is now rank {Ranking}!", "rank Up!");

            Ranking++;
        }
    }
}
