using System;
using Application.Services.MessageService;
using Application.Services.RaycastService;
using Application.Services.TranscationService;
using Cysharp.Threading.Tasks;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.GameState;
using Domain.Gameplay.Models.Grid;
using Infrastructure.Factories.Buildings;
using MessagePipe;
using Repositories.Building;
using UnityEngine;

namespace Application.UseCases
{
    internal sealed class SpawnBuildingUseCase : IDisposable
    {
        private readonly IPublisher<BuildingSpawnedMessageDto> _buildingSpawnedPublisher;
        private readonly IRaycastService _raycastService;
        private readonly Camera _camera;
        private readonly IBuildingFactory _buildingFactory;
        private readonly IGameStateChanger _gameStateChanger;
        private readonly ITransactionService _transactionService;
        private readonly IBuildingsRepository _buildingsRepository;
        private readonly IMessageService _messageService;

        private IDisposable _disposable;
        private IGridModel _gridModel;

        public SpawnBuildingUseCase(ISubscriber<ScreenTouchedMessageDto> screenTouchedSubscriber,
            IRaycastService raycastService, Camera camera, IBuildingFactory buildingFactory,
            ISubscriber<GridInitializedMessageDto> gridInitializedSubscriber, IGameStateChanger gameStateChanger,
            IPublisher<BuildingSpawnedMessageDto> buildingSpawnedPublisher, ITransactionService transactionService,
            IBuildingsRepository buildingsRepository, IMessageService messageService)
        {
            _raycastService = raycastService;
            _camera = camera;
            _buildingFactory = buildingFactory;
            _gameStateChanger = gameStateChanger;
            _buildingSpawnedPublisher = buildingSpawnedPublisher;
            _transactionService = transactionService;
            _buildingsRepository = buildingsRepository;
            _messageService = messageService;

            var bag = DisposableBag.CreateBuilder();

            screenTouchedSubscriber.Subscribe(OnScreenTouched).AddTo(bag);
            gridInitializedSubscriber.Subscribe(OnGridInitialized).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        private async void OnScreenTouched(ScreenTouchedMessageDto screenTouchedDto)
        {
            try
            {
                if (_gameStateChanger.CurrentState != GameState.Building)
                    return;

                var ray = _camera.ScreenPointToRay(screenTouchedDto.Position);
                var raycastHit = _raycastService.Raycast(ray);

                if (raycastHit.HasValue == false ||
                    raycastHit.Value.transform.gameObject.layer != LayerMask.NameToLayer("Cell"))
                    return;

                var position = raycastHit.Value.transform.position;

                if (_gridModel.IsCellEmpty((int)position.x, (int)position.z) == false)
                    return;

                if (_buildingsRepository.TryGetBuilding("Default", out var repository) == false)
                    return;

                if (_transactionService.HasMoneyToSpend(repository.Price) == false)
                {
                    _messageService.SendMessage("Not enough money to build!");
                    return;
                }

                if (_transactionService.TryRemoveMoney(repository.Price) == false)
                    return;
                
                position.y += raycastHit.Value.transform.localScale.y;

                await UniTask.Yield();

                var building = _buildingFactory.CreateBuilding("Default", position);

                _gridModel.OccupyCell((int)position.x, (int)position.z, building);
                _buildingSpawnedPublisher.Publish(new BuildingSpawnedMessageDto(building));
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void OnGridInitialized(GridInitializedMessageDto gridInitializedDto)
        {
            _gridModel = gridInitializedDto.GridModel;
        }
    }
}