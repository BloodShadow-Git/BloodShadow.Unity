using BloodShadow.GameCore.UI;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.UI
{
    public class UIPatternSet : UIPatternSet<GameObject>
    {
        public UIPatternSet(UISetData set) : base(set) { }
        public UIPatternSet(UIPattern[] panels, UIPattern[] loads) : base(panels, loads) { }
        public UIPatternSet(GameObject[] panels, GameObject[] loads) : base(panels, loads) { }
        public UIPatternSet(UISetData set, UIViewModel[] panelsVM, UIViewModel[] loadsVM) : base(set, panelsVM, loadsVM) { }
        public UIPatternSet(UISetData set, UIViewModel[] panelsVM, UIViewModel loadVM) : base(set, panelsVM, loadVM) { }
        public UIPatternSet(UISetData set, UIViewModel panelVM, UIViewModel[] loadsVM) : base(set, panelVM, loadsVM) { }
        public UIPatternSet(UISetData set, UIViewModel panelVM, UIViewModel loadVM) : base(set, panelVM, loadVM) { }
        public UIPatternSet(GameObject[] panels, UIViewModel[] panelsVM, GameObject[] loads, UIViewModel[] loadsVM)
            : base(panels, panelsVM, loads, loadsVM) { }
        public UIPatternSet(GameObject[] panels, UIViewModel panelVM, GameObject[] loads, UIViewModel[] loadsVM)
            : base(panels, panelVM, loads, loadsVM) { }
        public UIPatternSet(GameObject[] panels, UIViewModel[] panelsVM, GameObject[] loads, UIViewModel loadVM)
            : base(panels, panelsVM, loads, loadVM) { }
        public UIPatternSet(GameObject[] panels, UIViewModel panelVM, GameObject[] loads, UIViewModel loadVM)
            : base(panels, panelVM, loads, loadVM) { }
    }
}
