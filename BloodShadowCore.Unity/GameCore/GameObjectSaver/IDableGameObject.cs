using BloodShadow.GameCore.IDable;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.GameObjectSaver
{
    public class IDableGameObject : ScriptableObject, IID
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public GameObject GO { get; private set; }
    }
}
