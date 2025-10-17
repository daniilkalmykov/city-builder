using UnityEngine;

namespace Repositories.Camera
{
    [CreateAssetMenu(fileName = "CameraRepository", menuName = "Repositories/CameraRepository")]
    internal sealed class CameraRepository : ScriptableObject, ICameraRepository
    {
        [field: SerializeField]public float MoveSpeed { get; private set; }
        [field: SerializeField]public float MinFov { get; private set; }
        [field: SerializeField]public float MaxFov { get; private set; }
        [field: SerializeField]public float ZoomSpeed { get; private set; }
    }
}