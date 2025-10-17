using Domain.Gameplay.Models.GameState;

namespace Domain.Gameplay.MessagesDTO
{
    public struct GameStateChangedMessageDto
    {
        public GameStateChangedMessageDto(GameState gameState)
        {
            GameState = gameState;
        }

        public GameState GameState { get; }
    }
}