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

        public void RollDice(out int number)
        {
            number = dice.Next(1,7);
        }

        public Tuple<bool, SpecialField> IsSpecialField(int Field)
        {
            bool hasField = false;
            SpecialField specialField = null;
            foreach (SpecialField special in SpecialFields)
            {
                if (special.FieldNumber == Field)
                {
                    hasField = true;
                    specialField = special;
                }
            }
            return Tuple.Create(hasField, specialField);
        }

            private void GenerateSpecialFields()
        {
            SpecialFields.Add(new SpecialField(5, "Forward Goose", "go forwards two fields", SpecialField.CommandOptions.GoTO, 7));
            SpecialFields.Add(new SpecialField(6, "Bridge", "Continue to field 12.", SpecialField.CommandOptions.GoTO, 12));
            SpecialFields.Add(new SpecialField(9, "Forward Goose", "go forwards two fields", SpecialField.CommandOptions.GoTO, 11));
            SpecialFields.Add(new SpecialField(14, "Backwards Goose", "go back 2 fields", SpecialField.CommandOptions.GoTO, 12));
            SpecialFields.Add(new SpecialField(18, "Backwards Goose", "go back 2 fields", SpecialField.CommandOptions.GoTO, 16));
            SpecialFields.Add(new SpecialField(19, "Inn", "Skip a trun.", SpecialField.CommandOptions.SkipTurn, 0));
            SpecialFields.Add(new SpecialField(23, "backwards Goose", "go back 2 fields", SpecialField.CommandOptions.GoTO, 21));
            SpecialFields.Add(new SpecialField(26, "dice of doom", "skip a turn", SpecialField.CommandOptions.SkipTurn, 0));
            SpecialFields.Add(new SpecialField(27, "forward Goose", "go forwards 2 fields", SpecialField.CommandOptions.GoTO, 29));
            SpecialFields.Add(new SpecialField(31, "Pit", "You must wait here till another player ends on this field." +
                " when anoter player ends up here you are free to go. The other player must wait here now.", SpecialField.CommandOptions.Wait, 0));
            SpecialFields.Add(new SpecialField(32, "forward Goose", "go forwards 2 fields", SpecialField.CommandOptions.GoTO, 34));
            SpecialFields.Add(new SpecialField(41, "forward Goose", "go forwards 2 fields", SpecialField.CommandOptions.GoTO, 43));
            SpecialFields.Add(new SpecialField(42, "Maze", "go back to field 39.", SpecialField.CommandOptions.GoTO, 39));
            SpecialFields.Add(new SpecialField(45, "Backwards Goose", "go back 2 fields", SpecialField.CommandOptions.GoTO, 43));
            SpecialFields.Add(new SpecialField(50, "Backwards Goose", "go back 2 fields", SpecialField.CommandOptions.GoTO, 48));
            SpecialFields.Add(new SpecialField(52, "Prizon", "You must wait here till another player ends on this field." +
                " when anoter player ends up here you are free to go. The other player must wait here now.", SpecialField.CommandOptions.Wait, 0));
            SpecialFields.Add(new SpecialField(253, "dice of doom", "skip a turn", SpecialField.CommandOptions.SkipTurn, 0));
            SpecialFields.Add(new SpecialField(54, "Backwards Goose", "go back 3 fields", SpecialField.CommandOptions.GoTO, 51));
            SpecialFields.Add(new SpecialField(58, "Death", "Return to the start and try again.", SpecialField.CommandOptions.GoTO, 0));
            SpecialFields.Add(new SpecialField(59, "Backwards Goose", "go back 2 fields", SpecialField.CommandOptions.GoTO, 57));
            SpecialFields.Add(new SpecialField(63, "end", "You have won", SpecialField.CommandOptions.End, 0));

        }

    }
}
