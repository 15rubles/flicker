using System;
using Controller;
using Object.Monster;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class DebuffNextCardAbility : MonsterAbility
    {
        public int debuffValue = 1;
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            var nextCreature = ControllerLocator.GetService<BattleController>().NextAttackingCreature;
            if (nextCreature != null)
            {
                nextCreature.Power = debuffValue;
            }
            return this;
        }
    }
}