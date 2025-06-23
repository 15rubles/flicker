using System;

namespace Entity.Item.Ability
{
    [Serializable]
    public abstract class ItemAbility
    {
        public abstract ItemType ItemType { get; }

        public abstract void EnableItem();
        
        public abstract void DisableItem();

        public abstract void Trigger();
    }
}