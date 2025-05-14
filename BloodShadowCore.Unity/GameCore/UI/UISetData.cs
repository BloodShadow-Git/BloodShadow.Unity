using BloodShadow.GameCore.UI;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.UI
{
    public class UISetData : UISetData<GameObject>
    {
        public UISetData(GameObject[] panels, GameObject[] loadScreens) : base(panels, loadScreens) { }

        public static implicit operator UIPatternSet(UISetData data) => new UIPatternSet(data);
    }
}
