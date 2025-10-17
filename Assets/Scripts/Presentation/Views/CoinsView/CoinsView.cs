using UnityEngine.UIElements;

namespace Presentation.Views.CoinsView
{
    internal sealed class CoinsView : LayoutViewBase, ICoinsView
    {
        private Label _coinLabel;

        public override void Awake()
        {
            base.Awake();

            _coinLabel = root.Q<Label>("coin-count");
        }

        public void UpdateCoinCount(int coinCount)
        {
            if (_coinLabel != null) _coinLabel.text = coinCount.ToString();
        }
    }
}