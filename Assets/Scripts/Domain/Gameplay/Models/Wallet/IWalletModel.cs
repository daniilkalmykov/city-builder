using R3;

namespace Domain.Gameplay.Models.Wallet
{
    public interface IWalletModel
    {
        ReactiveProperty<int> Value { get; }
    }
}