using Infrastructure.InputSystem;
using Repositories.Camera;
using UnityEngine;
using VContainer.Unity;

namespace Application.UseCases
{
    public sealed class CameraMovementUseCase : ITickable
    {
        private readonly IInputSystem _inputSystem;
        private readonly Camera _camera;
        private readonly float _moveSpeed;
        
        public CameraMovementUseCase(Camera camera, IInputSystem inputSystem, ICameraRepository cameraRepository)
        {
            _camera = camera;
            _inputSystem = inputSystem;
            _moveSpeed = cameraRepository.MoveSpeed;
        }

        public void Tick()
        {
            var moveInput = new Vector2(_inputSystem.Horizontal, _inputSystem.Vertical); // например, (x = A/D, y = W/S)

            var moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

            _camera.transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        }
    }
}