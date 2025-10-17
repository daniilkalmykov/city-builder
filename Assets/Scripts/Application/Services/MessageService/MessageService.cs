using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;

namespace Application.Services.MessageService
{
    internal sealed class MessageService : IMessageService
    {
        private readonly IPublisher<MessageSentMessageDto> _publisher;

        private DateTime _lastMessageTime;

        public MessageService(IPublisher<MessageSentMessageDto> publisher)
        {
            _publisher = publisher;
        }

        public void SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message) != false || (DateTime.Now - _lastMessageTime).TotalSeconds < 1)
                return;

            _lastMessageTime = DateTime.Now;

            _publisher.Publish(new MessageSentMessageDto(message));
        }
    }
}