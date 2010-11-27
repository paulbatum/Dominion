using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominion.Rules.Activities
{
    public class ChoiceActivity : ActivityBase
    {
        public IEnumerable<Choice> AllowedOptions { get; private set; }

        public ChoiceActivity(TurnContext context, Player player, string message, ICard source, params Choice[] options)
            : this(context.Game.Log, player, message, source, options)
        {
        }

        public ChoiceActivity(IGameLog log, Player player, string message, ICard source, params Choice[] options)
            : base(log, player, message, ActivityType.MakeChoice, source)
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

        public override IDictionary<string, object> Properties
        {
            get
            {
                var properties = base.Properties;
                properties["AllowedOptions"] = AllowedOptions.Select(o => o.ToString()).ToList();
                return properties;
            }
        }
    }

    public enum Choice
    {
        DrawCards,
        GainActions,
        Yes,
        No
    }
}
