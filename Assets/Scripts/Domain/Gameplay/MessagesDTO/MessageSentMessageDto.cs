namespace Domain.Gameplay.MessagesDTO
{
    public struct MessageSentMessageDto
    {
        public MessageSentMessageDto(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}