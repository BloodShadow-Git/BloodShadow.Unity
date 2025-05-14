using UnityEngine;

namespace BloodShadow.Unity.GameCore.TimerManager
{
    using BloodShadow.GameCore.TimerManager;
    using System;

    public class DefaultTimerManager : MonoBehaviour
    {
        public TimerController TimerController { get; private set; }

        private void Awake() { TimerController = new TimerController(); }
        private void Update() { TimerController.UpdateWithDelta(Time.deltaTime); }

        public bool AddTimer(string name, TimerData data) => TimerController.AddTimer(name, data);
        public bool AddTimer(string name, float time, Action action, TimerMode mode = TimerMode.OneTime) => TimerController.AddTimer(name, time, action, mode);
        public bool RemoveTimer(string name) => TimerController.RemoveTimer(name);

        public static implicit operator TimerManager(DefaultTimerManager mgr) => mgr.TimerController;
    }
}
