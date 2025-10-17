using UnityEngine;

namespace Infrastructure.PlayerRepository
{
    [CreateAssetMenu(fileName = "PlayerRepository", menuName = "Repositories/PlayerRepository")]
    internal sealed class PlayerRepository : ScriptableObject, IPlayerRepository
    {
        [field: SerializeField] public int StartMoney { get; private set; }
    }
}