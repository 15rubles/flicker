using System.Collections.Generic;
using Entity.Card.Ability;

namespace Entity.Item
{
    public enum ItemType
    {
        None,
        Enter,
        Passive,
        TriggerBeginningOfCombat,
        TriggerEndOfCombat,
        TriggerCardPlayed,
        BattleWon,
        BattleLost
    }
    
    public static class ItemTypeData
    {
        private readonly static Dictionary<ItemType, string> Descriptions = new Dictionary<ItemType, string>
        {
            { ItemType.None, "" },
            { ItemType.Enter, "After Purchase" },
            { ItemType.Passive, "Passive" },
            { ItemType.TriggerBeginningOfCombat, "At the beginning of combat" },
            { ItemType.TriggerEndOfCombat, "At the end of combat" },
            { ItemType.TriggerCardPlayed, "When card played" },
            { ItemType.BattleWon, "If battle won" },
            { ItemType.BattleLost, "If battle lost" },
        };

        public static string GetDescription(this ItemType type)
        {
            return Descriptions.GetValueOrDefault(type, "Unknown");
        }
    }
}