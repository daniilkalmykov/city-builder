using System;
using Application.Services.RaycastService;
using Cysharp.Threading.Tasks;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Buildings;
using Domain.Gameplay.Models.GameState;
using Domain.Gameplay.Models.Grid;
using MessagePipe;
using UnityEngine;

namespace Application.UseCases
{
    internal sealed class MoveBuildingUseCase : IDisposable
    {
        private readonly IGameStateChanger _gameStateChanger;
        private readonly Camera _camera;
        private readonly IRaycastService _raycastService;
        private readonly IPublisher<BuildingMovedMessageDto> _buildingMovedPublisher;

        private IDisposable _disposable;
        private IBuildingModel _buildingModel;
        private int _x;
        private int _y;
        private GameObject _gameObject;
        private IGridModel _gridModel;

        public MoveBuildingUseCase(ISubscriber<BuildingSelectedMessageDto> buildingSelectedMessageSubscriber,
            ISubscriber<GridInitializedMessageDto> gridInitializedSubscriber,
            ISubscriber<ScreenTouchedMessageDto> screenTouchedSubscriber, IGameStateChanger gameStateChanger,
            Camera camera, IRaycastService raycastService, IPublisher<BuildingMovedMessageDto> buildingMovedPublisher)
        {
            _gameStateChanger = gameStateChanger;
            _camera = camera;
            _raycastService = raycastService;
            _buildingMovedPublisher = buildingMovedPublisher;
            var bag = DisposableBag.CreateBuilder();

            buildingSelectedMessageSubscriber.Subscribe(OnBuildingSelected).AddTo(bag);
            gridInitializedSubscriber.Subscribe(OnGridInitialized).AddTo(bag);
            screenTouchedSubscriber.Subscribe(OnScreenTouched).AddTo(bag);

            _disposable = bag.Build();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _disposable = null;
        }

        private void OnBuildingSelected(BuildingSelectedMessageDto buildingSelectedMessageDto)
        {
            _buildingModel = buildingSelectedMessageDto.BuildingModel;

            if (_buildingModel == null)
                return;

            _x = buildingSelectedMessageDto.X;
            _y = buildingSelectedMessageDto.Y;
            _gameObject = (GameObject)buildingSelectedMessageDto.Object;
        }

        private void OnGridInitialized(GridInitializedMessageDto gridInitializedMessageDto)
        {
            _gridModel = gridInitializedMessageDto.GridModel;
        }

        private async void OnScreenTouched(ScreenTouchedMessageDto screenTouchedMessageDto)
        {
            try
            {
                if (_gameStateChanger.CurrentState != GameState.SelectBuilding)
                    return;

                var ray = _camera.ScreenPointToRay(screenTouchedMessageDto.Position);
                var raycastHit = _raycastService.Raycast(ray);

                if (raycastHit.HasValue == false ||
                    raycastHit.Value.transform.gameObject.layer != LayerMask.NameToLayer("Cell"))
                    return;

                var position = raycastHit.Value.transform.position;

                if (_gridModel.IsCellEmpty((int)position.x, (int)position.z) == false)
                    return;

                position.y += raycastHit.Value.transform.localScale.y;

                await UniTask.Yield();

                _gridModel.ClearCell(_x, _y);
                _gridModel.OccupyCell((int)position.x, (int)position.z, _buildingModel);
                _gameObject.transform.position = position;

                _buildingMovedPublisher.Publish(new BuildingMovedMessageDto());
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}