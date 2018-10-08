using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanzenBord
{
    class GameLogics
    {
        private Random dice;
        public List<SpecialField> SpecialFields {get;}
        private static GameLogics Instance;

        private GameLogics()
        {
            SpecialFields = new List<SpecialField>();
            GenerateSpecialFields();
            dice = new Random();
        }

        public static GameLogics GetInstance()
        {
            if (Instance == null)
                Instance = new GameLogics();

            return Instance;
        }

        public void RollDice(out int DiceResult)
        {
            DiceResult = dice.Next(1,7);
        }

        private void GenerateSpecialFields()
        {
            SpecialFields.Add(new SpecialField(6, "Bridge", "Continue to field 12.", SpecialField.CommandOptions.GoTO, 12));
            SpecialFields.Add(new SpecialField(19, "Inn", "Skip a trun.", SpecialField.CommandOptions.SkipTurn, 0));
            SpecialFields.Add(new SpecialField(31, "Pit", "You must wait here till another player ends on this field." +
                " when anoter player ends up here you are free to go. The other player must wait here now.", SpecialField.CommandOptions.Wait, 0));
            SpecialFields.Add(new SpecialField(42, "Maze", "go back to field 39.", SpecialField.CommandOptions.GoTO, 39));
            SpecialFields.Add(new SpecialField(52, "Prizon", "You must wait here till another player ends on this field." +
                " when anoter player ends up here you are free to go. The other player must wait here now.", SpecialField.CommandOptions.Wait, 0));
            SpecialFields.Add(new SpecialField(58, "Death", "Return to the start and try again.", SpecialField.CommandOptions.GoTO, 0));
            SpecialFields.Add(new SpecialField(63, "end", "You have won", SpecialField.CommandOptions.End, 0));

        }

    }
}
