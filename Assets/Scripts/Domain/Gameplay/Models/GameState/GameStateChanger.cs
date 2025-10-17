namespace Domain.Gameplay.Models.GameState
{
    internal sealed class GameStateChanger : IGameStateChanger
    {
        public GameState CurrentState { get; private set; } = GameState.Building;

        public void Change(GameState newState)
        {
            CurrentState = newState;
        }
    }
}