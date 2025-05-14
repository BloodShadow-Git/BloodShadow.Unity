using BloodShadow.Core.SaveSystem;
using System.IO;
using UnityEngine;

namespace BloodShadow.Unity.Core.SaveSystem
{
    public class UnityJsonSaveSystem : JsonSaveSystem
    {
        protected override string BuildPath(string key) => Path.Combine(Application.persistentDataPath, key);
    }
}
