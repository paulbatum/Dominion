using Dominion.Rules;

namespace Dominion.GameHost
{
    public interface IGameActionMessage
    {
        void UpdateGameState(Game game);
        void Validate(Game game);
    }
}