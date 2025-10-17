using Infrastructure.InputSystem;
using Repositories.Camera;
using UnityEngine;
using VContainer.Unity;

namespace Application.UseCases
{
    internal sealed class CameraZoomUseCase : ITickable
    {
        private readonly IInputSystem _inputSystem;
        private readonly Camera _camera;
        private readonly float _zoomSpeed;
        private readonly float _minFov;
        private readonly float _maxFov;
        
        public CameraZoomUseCase(Camera camera, IInputSystem inputSystem, ICameraRepository cameraRepository)
        {
            _camera = camera;
            _inputSystem = inputSystem;
            _zoomSpeed = cameraRepository.ZoomSpeed;
            _minFov = cameraRepository.MinFov;
            _maxFov = cameraRepository.MaxFov;
        }

        public void Tick()
        {
            var scroll = _inputSystem.Scroll;

            if (Mathf.Abs(scroll) < 0.01f) 
                return;
            
            if (_camera.orthographic)
            {
                _camera.orthographicSize -= scroll * _zoomSpeed * Time.deltaTime;
                _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _minFov, _maxFov);
            }
            else 
            {
                _camera.transform.position += _camera.transform.forward * scroll * _zoomSpeed * Time.deltaTime;
            }
        }
    }
}