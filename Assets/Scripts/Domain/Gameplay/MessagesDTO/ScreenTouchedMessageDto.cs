using UnityEngine;

namespace Domain.Gameplay.MessagesDTO
{
    public struct ScreenTouchedMessageDto
    {
        public ScreenTouchedMessageDto(Vector3 position)
        {
            Position = position;
        }

        public Vector3 Position { get; }
    }
}