using BloodShadow.GameCore.InventorySystem.Recipes.Stations;

namespace BloodShadow.Unity.GameCore.InventorySystem.Recipes.Stations
{
    using BloodShadow.Unity.GameCore.InventorySystem.Items;

    public abstract class ListStation : ListStation<UnityItem>
    {
        protected ListStation(string localizationKey) : base(localizationKey) { }
    }
}
