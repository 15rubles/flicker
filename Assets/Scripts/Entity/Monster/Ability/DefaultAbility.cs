using System;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class DefaultAbility : MonsterAbility
    {
        public override MonsterAbility UseAbility()
        {
            return this;
        }
    }
}