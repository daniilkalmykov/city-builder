using Domain.Gameplay.Models.Messages;
using UnityEngine;

namespace Application.Services.MessageService
{
    internal sealed class MessageService : IMessageService
    {
        private readonly IMessageModel _messageModel;

        public MessageService(IMessageModel messageModel)
        {
            _messageModel = messageModel;
        }

        public void SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message) == false && string.Equals(message, _messageModel.Message.Value) == false)
            {
                _messageModel.Message.Value = message;
            }
        }
    }
}