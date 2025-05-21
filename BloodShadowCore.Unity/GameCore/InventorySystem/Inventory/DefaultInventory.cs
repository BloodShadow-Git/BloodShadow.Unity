using BloodShadow.GameCore.InventorySystem.Inventory;
using BloodShadow.Unity.GameCore.InventorySystem.Items;
using System.Collections.Generic;

namespace BloodShadow.Unity.GameCore.InventorySystem.Inventory
{
    public class DefaultInventory : DefaultInventory<UnityItem>
    {
        public DefaultInventory(string key) : base(key) { }
        public DefaultInventory(string key, IEnumerable<InventoryData<UnityItem>> items) : base(key, items) { }
    }
}
