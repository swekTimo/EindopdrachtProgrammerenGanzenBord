﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bord
{
    class Player
    {
        public int PlayerID { get; }
        public int Field { get; set; }
        public int Ranking { get; set; }
        private GameLogics Logics;

        private bool PlayNextTrun;
        public bool Wait { get; set; }
        public bool hasWon;

        public Player(int playerID, int ranking)
        {
            this.PlayerID = playerID;
            this.Field = 0;
            this.Ranking = ranking;

            Logics = GameLogics.GetInstance();
            PlayNextTrun = true;
            Wait = false;
            hasWon = false;
        }

        public void MoveGoose()
        {
            int amountOfSpaces;
            Logics.RollDice(out amountOfSpaces);
            if (PlayNextTrun == false)
            {
                if (Wait == false)
                    PlayNextTrun = true; 
            }
            else
            {
                int tempField = Field;

                if (tempField + amountOfSpaces > 63)
                {
                    this.Field = 63 - (amountOfSpaces - (63 - tempField));
                }
                else
                    this.Field = Field + amountOfSpaces;
                Tuple<bool, SpecialField> SpecialField = Logics.IsSpecialField(Field);
                if (SpecialField.Item1 == true)
                    ActionOfField(SpecialField.Item2);
            }
        }

        private void ActionOfField(SpecialField special)
        {
            if (special.Command == SpecialField.CommandOptions.GoTO)
            {
                Field = special.GoToFIeld;
            }
            else if (special.Command == SpecialField.CommandOptions.SkipTurn)
            {
                PlayNextTrun = false;
            }
            else if (special.Command == SpecialField.CommandOptions.Wait)
            {
                PlayNextTrun = false;
                Wait = true;
            }
            else if (special.Command == SpecialField.CommandOptions.End)
            {
                hasWon = true;
            }
        }
    }
}
