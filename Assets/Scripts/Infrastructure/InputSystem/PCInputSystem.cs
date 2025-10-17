using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.InputSystem
{
    internal sealed class PCInputSystem : IInputSystem, ITickable
    {
        private readonly IPublisher<ScreenTouchedMessageDto> _screenTouchedPublisher;
        private readonly IPublisher<DeleteButtonTouchedMessageDto> _deleteButtonTouchedPublisher;
        private readonly IPublisher<CancelOperationButtonClickedMessageDto> _cancelOperationButtonClickedPublisher;
        private readonly IPublisher<UpgradeButtonClickedMessageDto> _upgradeButtonClickedPublisher;
        private readonly IPublisher<NumberButtonPressedMessageDto> _numberButtonPressedPublisher;

        public PCInputSystem(IPublisher<ScreenTouchedMessageDto> screenTouchedPublisher,
            IPublisher<DeleteButtonTouchedMessageDto> deleteButtonTouchedPublisher,
            IPublisher<CancelOperationButtonClickedMessageDto> cancelOperationButtonClickedPublisher,
            IPublisher<UpgradeButtonClickedMessageDto> upgradeButtonClickedPublisher,
            IPublisher<NumberButtonPressedMessageDto> numberButtonPressedPublisher)
        {
            _screenTouchedPublisher = screenTouchedPublisher;
            _deleteButtonTouchedPublisher = deleteButtonTouchedPublisher;
            _cancelOperationButtonClickedPublisher = cancelOperationButtonClickedPublisher;
            _upgradeButtonClickedPublisher = upgradeButtonClickedPublisher;
            _numberButtonPressedPublisher = numberButtonPressedPublisher;
        }

        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        public float Scroll { get; private set; }

        public void Tick()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
            Scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Input.GetMouseButtonDown(0))
            {
                _screenTouchedPublisher.Publish(new ScreenTouchedMessageDto(Input.mousePosition));
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                _deleteButtonTouchedPublisher.Publish(new DeleteButtonTouchedMessageDto());
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                _cancelOperationButtonClickedPublisher.Publish(new CancelOperationButtonClickedMessageDto());
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                _upgradeButtonClickedPublisher.Publish(new UpgradeButtonClickedMessageDto());
            }
            else
            {
                for (var i = 1; i < 9; i++)
                {
                    if (Input.GetKeyDown(i.ToString()))
                        _numberButtonPressedPublisher.Publish(new NumberButtonPressedMessageDto(i));
                }
            }
        }
    }
}