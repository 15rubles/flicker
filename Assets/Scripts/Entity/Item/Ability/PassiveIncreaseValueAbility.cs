using System;
namespace Entity.Item.Ability
{
    [Serializable]
    public class PassiveIncreaseValueAbility : ItemAbility
    {
        public override ItemType ItemType => ItemType.Passive;
        public override void EnableItem()
        {
            
        }
        public override void DisableItem()
        {
            
        }
        public override void Trigger()
        {}
    }
}