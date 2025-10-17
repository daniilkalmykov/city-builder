using UnityEngine;
using UnityEngine.UIElements;

namespace Presentation.Views.MessageView
{
    internal sealed class MessageView : LayoutViewBase, IMessageView
    {
        private Label _messageLabel;
        private VisualElement _messageElement;

        public override void Awake()
        {
            base.Awake();
            
            _messageElement = root.Q<VisualElement>("message-view");
            
            if (_messageElement == null)
            {
                Debug.LogError("MessageView: visual element 'message-view' not found");
                return;
            }

            _messageElement.style.translate = new StyleTranslate(
                new Translate(new Length(-50, LengthUnit.Percent), 0)
            );
            
            _messageLabel = _messageElement.Q<Label>("message-label");
            
            if (_messageLabel == null)
                Debug.LogError("MessageView: label 'message-label' not found");
        }

        public void Show(string message)
        {
            if (_messageElement == null) 
                return;

            if (string.IsNullOrEmpty(message) == false && _messageLabel != null)
                _messageLabel.text = message;

            _messageElement.AddToClassList("visible");
        }

        public void Hide()
        {
            root.RemoveFromClassList("visible");
        }
    }
}