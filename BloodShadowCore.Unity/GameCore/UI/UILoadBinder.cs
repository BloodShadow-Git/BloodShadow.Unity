using BloodShadow.Core.Operations;
using BloodShadow.GameCore.UI;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.UI
{
    public abstract class UILoadBinder<TViewModel> : UIBinder<TViewModel>, IUILoadBinder<GameObject> where TViewModel : UIViewModel
    {
        protected Operation Operation;
        public void InternalEnable(Operation op) { if (op != null) { Operation = op; } }
    }
}
