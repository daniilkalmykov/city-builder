using System;
using Application.Services.RaycastService;
using Domain.Gameplay.MessagesDTO;
using Domain.Gameplay.Models.Grid;
using MessagePipe;
using UnityEngine;

namespace Application.UseCases
{
    internal sealed class SelectBuildingUseCase : IDisposable
    {
        private readonly IRaycastService _raycastService;
        private readonly Camera _camera;
        private readonly IPublisher<BuildingSelectedMessageDto> _buildSelectedMessagePublisher;

        private IGridModel _gridModel;
        private IDisposable _disposable;

        public SelectBuildingUseCase(ISubscriber<ScreenTouchedMessageDto> screenTouchedSubscriber,
            IRaycastService raycastService, Camera camera,
            ISubscriber<GridInitializedMessageDto> gridInitializedSubscriber,
            IPublisher<BuildingSelectedMessageDto> buildSelectedMessagePublisher)
        {
            _raycastService = raycastService;
            _camera = camera;
            _buildSelectedMessagePublisher = buildSelectedMessagePublisher;

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

        private void OnScreenTouched(ScreenTouchedMessageDto screenTouchedDto)
        {
            var ray = _camera.ScreenPointToRay(screenTouchedDto.Position);
            var raycastHit = _raycastService.Raycast(ray);

            if (raycastHit.HasValue == false ||
                raycastHit.Value.transform.gameObject.layer != LayerMask.NameToLayer("Building"))
            {
                return;
            }

            var position = raycastHit.Value.transform.position;
            var buildingModel = _gridModel.GetBuildingAt((int)position.x, (int)position.z);

            if (buildingModel != null)
            {
                _buildSelectedMessagePublisher.Publish(new BuildingSelectedMessageDto(buildingModel, (int)position.x,
                    (int)position.z, raycastHit.Value.transform.gameObject));
            }
        }

        private void OnGridInitialized(GridInitializedMessageDto gridInitializedDto)
        {
            _gridModel = gridInitializedDto.GridModel;
        }
    }
}