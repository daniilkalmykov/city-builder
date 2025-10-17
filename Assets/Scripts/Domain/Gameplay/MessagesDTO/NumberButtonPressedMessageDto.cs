namespace Domain.Gameplay.MessagesDTO
{
    public struct NumberButtonPressedMessageDto
    {
        public NumberButtonPressedMessageDto(int index)
        {
            Index = index;
        }

        public int Index { get; }
    }
}