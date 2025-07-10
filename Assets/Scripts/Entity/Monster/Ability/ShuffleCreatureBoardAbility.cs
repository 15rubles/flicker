using System.Collections.Generic;
using Controller;
using Object.Creature;
using Object.Monster;
using Utils;

namespace Entity.Monster.Ability
{
    public class ShuffleCreatureBoardAbility : MonsterAbility
    {

        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            var creatureController = ControllerLocator.GetService<CreatureController>();

            List<CreatureSlot> creatureSlots = creatureController.AttackZone.CreatureSlots;
            
            creatureSlots.Shuffle();

            for (int index = 0; index < creatureSlots.Count; index++)
            {
                creatureSlots[index].transform.SetSiblingIndex(index);
            }
            return this;
        }
    }
}