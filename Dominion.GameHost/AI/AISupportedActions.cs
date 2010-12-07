using System.Collections.Generic;
using System.Linq;
using Dominion.Cards.Actions;
using Dominion.Cards.Hybrid;
using Dominion.Rules;

namespace Dominion.GameHost.AI
{
    public static class AISupportedActions
    {
        public static IList<string> All = GetAll();
        public static IList<string> PlusActions = GetPlusActions();
        public static IList<string> PlusCards = GetPlusCards();
        public static IList<string> Others = GetOthers();

        private static IList<string> GetAll()
        {
            return GetPlusActions()
                .Concat(GetPlusCards())
                .Concat(GetOthers())
                .ToList();
        }


        private static IList<string> GetPlusActions()
        {
            var list = new List<string>();
            list.AddSimpleAction<Bazaar>();
            list.AddSimpleAction<Caravan>();
            list.AddSimpleAction<City>();            
            list.AddSimpleAction<Festival>();
            list.AddSimpleAction<FishingVillage>();
            list.AddSimpleAction<GreatHall>();
            list.AddSimpleAction<Laboratory>();
            list.AddSimpleAction<Market>();
            list.AddSimpleAction<ShantyTown>();
            list.AddSimpleAction<Village>();
            list.AddSimpleAction<Warehouse>();
            return list;
        }

        private static IList<string> GetPlusCards()
        {
            var list = new List<string>();
            list.AddSimpleAction<CouncilRoom>();
            list.AddSimpleAction<Envoy>();            
            list.AddSimpleAction<GhostShip>();
            list.AddSimpleAction<Moat>();
            list.AddSimpleAction<Rabble>();
            list.AddSimpleAction<Smithy>();
            list.AddSimpleAction<Witch>();
            list.AddSimpleAction<Masquerade>();
            
            return list;
        }

        private static IList<string> GetOthers()
        {
            var list = new List<string>();
            list.AddSimpleAction<Adventurer>();            
            list.AddSimpleAction<CountingHouse>();
            list.AddSimpleAction<Militia>();
            list.AddSimpleAction<Moneylender>();
            list.AddSimpleAction<Mountebank>();
            list.AddSimpleAction<SeaHag>();
            list.AddSimpleAction<Woodcutter>();
            list.AddSimpleAction<ThroneRoom>();
            // Chapel behavior has to be more sophisticated before it can be turned on.
            // list.AddSimpleAction<Chapel>(); 
            return list;
        }

        private static void AddSimpleAction<T>(this IList<string> list) where T : Card
        {
            list.Add(typeof(T).Name);            
        }
    }
}