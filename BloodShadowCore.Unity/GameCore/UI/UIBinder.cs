﻿using BloodShadow.GameCore.UI;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.UI
{
    public abstract class UIBinder<TViewModel> : MonoBehaviour, IUIBinder<GameObject> where TViewModel : UIViewModel
    {
        protected UIController<GameObject> Controller { get; private set; }
        protected int PanelIndex { get; private set; }
        protected TViewModel ViewModel { get; private set; }

        public void Bind(UIController<GameObject> controller, int index, UIViewModel<GameObject> viewModel)
        {
            if (viewModel.GetType() != typeof(TViewModel)) { return; }
            Controller = controller;
            PanelIndex = index;
            ViewModel = (TViewModel)viewModel;
            OnBind();
        }
        public virtual void OnBind() { }
        public virtual void Disable() { }
        public virtual void Dispose() { }
        public virtual void Enable() { }
        public virtual void InternalReset() { }
    }
}
