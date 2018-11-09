using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bord
{
    class SpecialField
    {
        public int FieldNumber { get; }
        public string FieldName { get;  }
        public string Description { get; }
        public CommandOptions Command { get; }
        public int GoToFIeld;

        public enum CommandOptions
        {
            GoTO,
            SkipTurn,
            Wait,
            End
        }

        public SpecialField(int fieldNumber, string fieldName, string description, CommandOptions command, int? goToField)
        {
            this.FieldNumber = fieldNumber;
            this.FieldName = fieldName;
            this.Description = description;
            this.Command = command;
            this.GoToFIeld = goToField ?? 0;
        }

        public override string ToString()
        {
            return $"{FieldName}({FieldNumber}) - {Description}";
        }
    }
}
