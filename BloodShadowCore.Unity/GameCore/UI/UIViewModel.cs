using BloodShadow.GameCore.UI;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.UI
{
    public abstract class UIViewModel : UIViewModel<GameObject>
    {
        protected UIViewModel(UIController controller) : base(controller) { }
    }
}
