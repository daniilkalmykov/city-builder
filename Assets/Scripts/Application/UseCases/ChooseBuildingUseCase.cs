using System;
using Domain.Gameplay.MessagesDTO;
using MessagePipe;
using Repositories.Building;
using UnityEngine;

namespace Application.UseCases
{
    internal sealed class ChooseBuildingUseCase : IDisposable
    {
        private readonly IBuildingsRepository _buildingsRepository;
        private readonly IPublisher<BuildingChosenMessageDto> _buildingChosenMessagePublisher;

        private IDisposable _disposable;

        public ChooseBuildingUseCase(ISubscriber<NumberButtonPressedMessageDto> numberButtonPressedSubscriber,
            IBuildingsRepository buildingsRepository,
            IPublisher<BuildingChosenMessageDto> buildingChosenMessagePublisher)
        {
            _buildingsRepository = buildingsRepository;
            _buildingChosenMessagePublisher = buildingChosenMessagePublisher;
            var bag = DisposableBag.CreateBuilder();

            numberButtonPressedSubscriber.Subscribe(OnNumberButtonPressed).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        private void OnNumberButtonPressed(NumberButtonPressedMessageDto msg)
        {
            if (_buildingsRepository.Buildings.Count < msg.Index)
            {
                _buildingChosenMessagePublisher.Publish(new BuildingChosenMessageDto(null));
                return;
            }

            var repository = _buildingsRepository.Buildings[msg.Index - 1];

            _buildingChosenMessagePublisher.Publish(new BuildingChosenMessageDto(repository));
        }
    }
}