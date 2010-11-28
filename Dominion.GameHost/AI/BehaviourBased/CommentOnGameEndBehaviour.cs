namespace Dominion.GameHost.AI.BehaviourBased
{
    public class CommentOnGameEndBehaviour : IAIBehaviour
    {
        private readonly string _comment;

        public CommentOnGameEndBehaviour(string comment)
        {
            _comment = comment;
        }

        public virtual bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return state.Status.GameIsComplete;
        }

        public virtual void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            client.SendChatMessage(_comment);
        }
    }
}