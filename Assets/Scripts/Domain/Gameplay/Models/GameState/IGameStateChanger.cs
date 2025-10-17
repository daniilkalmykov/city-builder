namespace Domain.Gameplay.Models.GameState
{
    public interface IGameStateChanger
    {
        GameState CurrentState { get; }
        
        void Change(GameState newState);
    }
}