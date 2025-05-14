using BloodShadow.GameCore.InventorySystem.Items;
using BloodShadow.Unity.GameCore.GameObjectSaver;
using Newtonsoft.Json;
using UnityEngine;

namespace BloodShadow.Unity.GameCore.InventorySystem.Items
{
    public abstract class UnityItem : Item
    {
        public override byte[] Icon => Sprite.texture.EncodeToPNG();
        public string PrefabID => Prefab.ID;
        [field: SerializeField][JsonIgnore] public Sprite Sprite { get; protected set; }
        [field: SerializeField][JsonIgnore] public IDableGameObject Prefab { get; protected set; }
    }
}
