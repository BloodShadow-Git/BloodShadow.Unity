using BloodShadow.Core.SaveSystem;
using BloodShadow.GameCore.Settings;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.Settings
{
    public class SettingManager : SettingManager<GameObject>
    {
        public SettingManager(string savePath) : base(savePath) { }
        public SettingManager(string savePath, SaveSystem saveSystem) : base(savePath, saveSystem) { }
    }
}
