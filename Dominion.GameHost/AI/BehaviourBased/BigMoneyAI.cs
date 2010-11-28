using System;
using System.Linq;
using System.Collections.Generic;
using Dominion.Cards.Actions;
using Dominion.Rules;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BigMoneyAI : BehaviourBasedAI
    {
        public BigMoneyAI()
        {
            Behaviours.Add(new CommentOnGameEndBehaviour("The game is over? But I was about to buy a Province!"));

            Behaviours.Add(new DefaultDiscardCardsBehaviour());
            Behaviours.Add(new DefaultMakeChoiceBehaviour());
            Behaviours.Add(new DefaultPassCardsBehaviour());
            Behaviours.Add(new DefaultSelectFromRevealedBehaviour());

            Behaviours.Add(new BuyPointsBehaviour(6));
            Behaviours.Add(new BigMoneyBuyBehaviour());
            Behaviours.Add(new SkipActionsBehaviour());
        }
    }

    public class PlaySimpleCardsBehaviour : IAIBehaviour
    {
        public IList<string> SimpleCards { get; set; }

        public PlaySimpleCardsBehaviour()
        {
            SimpleCards = new List<string>();

            AddSimpleCard<Adventurer>();
            AddSimpleCard<Bazaar>();
            AddSimpleCard<Caravan>();
            AddSimpleCard<Chancellor>();
            AddSimpleCard<City>();            
            AddSimpleCard<CouncilRoom>();
            AddSimpleCard<CountingHouse>();
            AddSimpleCard<Envoy>();            
            AddSimpleCard<Festival>();
            AddSimpleCard<FishingVillage>();
            AddSimpleCard<GhostShip>();
            AddSimpleCard<Laboratory>();
            AddSimpleCard<Market>();
            AddSimpleCard<Militia>();
            AddSimpleCard<Mine>();
            AddSimpleCard<Moat>();
            AddSimpleCard<Moneylender>();
            AddSimpleCard<Mountebank>();
            AddSimpleCard<Rabble>();
            AddSimpleCard<SeaHag>();
            AddSimpleCard<ShantyTown>();
            AddSimpleCard<Smithy>();
            AddSimpleCard<Village>();
            AddSimpleCard<Warehouse>();
            AddSimpleCard<Witch>();
            AddSimpleCard<Woodcutter>();
        }

        protected void AddSimpleCard<T>() where T : Card
        {
            SimpleCards.Add(typeof(T).Name);
        }

        public bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return activity.ParseType() == ActivityType.PlayActions &&
                   state.Hand.Select(c => c.Name).Intersect(SimpleCards).Any();
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            throw new NotImplementedException();
        }
    }
}