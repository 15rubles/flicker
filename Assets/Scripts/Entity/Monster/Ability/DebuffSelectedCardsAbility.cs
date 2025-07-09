using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Object.Creature;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class DebuffSelectedCardsAbility : MonsterAbility
    {
        public CreatureTarget creatureTarget = CreatureTarget.First;
        public int quantityOfCreaturesToDebuff = 1;
        public bool assignDebuffValueToPower = false;
        public int debuffValue = 1;

        public override MonsterAbility UseAbility()
        {
            var creatureController = ControllerLocator.GetService<CreatureController>();
            List<CreatureObj> creatures = new List<CreatureObj>();
            switch (creatureTarget)
            {
                case CreatureTarget.First:
                    creatures = creatureController.AttackZone.Creatures.Take(quantityOfCreaturesToDebuff).ToList();
                    break;
                case CreatureTarget.Last:
                    var list = creatureController.AttackZone.Creatures;
                    if (list != null)
                    {
                        creatures = list.AsEnumerable().Reverse().Take(quantityOfCreaturesToDebuff).ToList();
                    }
                    break;
                case CreatureTarget.Weakest:
                    creatures = creatureController.AttackZone.Creatures
                                                  .OrderBy(creature => creature.Power)
                                                  .Take(quantityOfCreaturesToDebuff).ToList();
                    break;
                case CreatureTarget.Strongest:
                    creatures = creatureController.AttackZone.Creatures
                                                  .OrderByDescending(creature => creature.Power)
                                                  .Take(quantityOfCreaturesToDebuff).ToList();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var creature in creatures)
            {
                if (assignDebuffValueToPower)
                {
                    creature.Power = debuffValue;
                }
                else
                {
                    creature.Power -= debuffValue;
                }
            }
            return this;
        }

        [Serializable]
        public enum CreatureTarget
        {
            First,
            Last,
            Weakest,
            Strongest
        }
    }
}