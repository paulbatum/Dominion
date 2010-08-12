namespace Dominion.Web.ViewModels
{
    public class PlayerActionResultViewModel
    {
        public bool Success { get; set; }
        public GameViewModel GameState { get; set; }
        public string ErrorMessage { get; set; }
    }
}