using BloodShadow.GameCore.InventorySystem.Recipes.Stations;

namespace BloodShadow.Unity.GameCore.InventorySystem.Recipes.Stations
{
    using BloodShadow.Unity.GameCore.InventorySystem.Items;

    public abstract class QueueStation : QueueStation<UnityItem>
    {
        protected QueueStation(string localizationKey) : base(localizationKey) { }
    }
}
