using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Rules.Activities
{
    public class ChoiceActivity : ActivityBase
    {
        public IEnumerable<Choice> AllowedOptions { get; private set; }

        public ChoiceActivity(TurnContext context, Player player, string message, params Choice[] options)
            : base(context.Game.Log, player, message, ActivityType.MakeChoice)
        {
            AllowedOptions = options;
        }

        public void MakeChoice(string optionString)
        {
            var option = (Choice)Enum.Parse(typeof(Choice), optionString);
            if (!AllowedOptions.Contains(option))
            {
                string error = string.Format("Player chose '{0}', expected something from list '{1}'",
                    option,
                    string.Join(", ", AllowedOptions.Select(o => o.ToString()).ToArray()));
                throw new Exception(error);
            }

            ActOnChoice(option);

            IsSatisfied = true;
        }

        public Action<Choice> ActOnChoice { get; set; }
    }

    public enum Choice
    {
        DrawCards,
        GainActions,
        Yes,
        No
    }
}
