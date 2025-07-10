using System;
using Object.Monster;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class DefaultAbility : MonsterAbility
    {
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            return this;
        }
    }
}