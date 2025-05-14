namespace BloodShadow.Unity.GameCore.TimerManager
{
    using BloodShadow.GameCore.TimerManager;
    using System.Collections.Generic;

    public class TimerController : TimerManager
    {
        public override void Update() => UpdateWithDelta(0);

        public void UpdateWithDelta(in float delta)
        {
            if (delta <= 0) { return; }
            List<string> timersToRemove = new List<string>();
            foreach (KeyValuePair<string, TimerData> timer in _timers)
            {
                timer.Value.EliminatedTime += delta;
                if (timer.Value.EliminatedTime >= timer.Value.Time)
                {
                    timer.Value.Action?.Invoke();
                    switch (timer.Value.Mode)
                    {
                        default:
                        case TimerMode.OneTime: timersToRemove.Add(timer.Key); break;
                        case TimerMode.Period: timer.Value.EliminatedTime -= timer.Value.Time; break;
                    }
                }
            }
            foreach (string timer in timersToRemove) { _timers.Remove(timer); }
        }
    }
}
