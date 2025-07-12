using System;
using Controller;
using Object.Creature;
using Object.Monster;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class KillCreatureOnDamageAbility : MonsterAbility
    {

        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            CreatureObj currentAttackingCreature =
                ControllerLocator.GetService<BattleController>().CurrentAttackingCreatureObj;

            if (currentAttackingCreature != null && monsterObject.LastDamage > 0)
            {
                currentAttackingCreature.DestroyCreature();
            }
            return this;
        }
    }
}