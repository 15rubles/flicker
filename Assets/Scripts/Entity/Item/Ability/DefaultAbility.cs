using System;

namespace Entity.Item.Ability
{
    [Serializable]
    public class DefaultAbility : ItemAbility
    {
        public int param;
        public override ItemType ItemType => ItemType.None;
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