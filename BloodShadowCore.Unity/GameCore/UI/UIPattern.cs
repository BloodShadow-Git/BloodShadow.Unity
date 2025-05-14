using BloodShadow.GameCore.UI;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.UI
{
    public class UIPattern : UIPattern<GameObject>
    {
        public UIPattern(GameObject viewScreen) : base(viewScreen) { }
        public UIPattern(GameObject viewScreen, UIViewModel viewModel) : base(viewScreen, viewModel) { }
        public static implicit operator UIPattern(GameObject screen) => new UIPattern(screen);
    }
}
