using System;

namespace Entity.Monster.Ability
{
    [Serializable]
    public abstract class MonsterAbility
    {
        public abstract MonsterAbility UseAbility();
    }
}