using System;

namespace Entity.Item.Ability
{
    [Serializable]
    public class TriggerCardPlayedAbility : ItemAbility
    {
        public override ItemType ItemType => ItemType.TriggerCardPlayed;
        public override void EnableItem()
        {
            
        }
        public override void DisableItem()
        {
            
        }
        public override void Trigger()
        {
           
        }
    }
}