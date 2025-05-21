using BloodShadow.GameCore.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.Settings
{
    public class SettingManager : SettingManager<GameObject>
    {
        public SettingManager() : base() { }
        public SettingManager(Dictionary<string, SettingData<GameObject>> data) : base(data) { }
    }
}
