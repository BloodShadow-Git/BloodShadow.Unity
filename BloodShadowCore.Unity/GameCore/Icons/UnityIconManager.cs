using BloodShadow.GameCore.Icons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodShadow.Unity.GameCore.Icons
{
    public class UnityIconManager : IconManager
    {
        private readonly char[] separator = { '<', '>', '/' };

        public UnityIconManager() : base() { }
        public UnityIconManager(Dictionary<string, DeviceIcon> icons) : base(icons) { }

        protected override string CombinePath(string deviceName, string key) => $"<{deviceName}>/{key}";

        protected override void SeparatePath(string path, out string deviceName, out string key)
        {
            IEnumerable<string> device = path.Split(separator).Where(str => !string.IsNullOrEmpty(str));
            deviceName = "ERROR";
            key = "ERROR";
            if (device.Count() == 2)
            {
                deviceName = device.ElementAt(0);
                key = device.ElementAt(1);
            }
        }
    }
}
