using System;
using System.Linq;
using Controller;
using Object.Creature;
using UnityEngine;
using Utils;

namespace Entity.Item.Ability
{
    [Serializable]
    public class BeginningOfCombatCardBuffAbility : ItemAbility
    {
        [SerializeField] private CreatureTarget creatureTarget;
        [SerializeField] private int buffValue = 1;
        public override ItemType ItemType => ItemType.TriggerBeginningOfCombat;
        public override void EnableItem() {}
        public override void DisableItem() {}
        public override void Trigger()
        {
            var creatureController = ControllerLocator.GetService<CreatureController>();
            CreatureObj modifiedCreature;
            switch (creatureTarget)
            {
                case CreatureTarget.Weakest:
                    modifiedCreature = creatureController.AttackZone.Creatures
                                                         .OrderBy(creature => creature.Power).FirstOrDefault();
                    if (modifiedCreature != null)
                        modifiedCreature.Power += buffValue;
                    break;
                case CreatureTarget.Strongest:
                    modifiedCreature = creatureController.AttackZone.Creatures
                                                         .OrderByDescending(creature => creature.Power).FirstOrDefault();
                    if (modifiedCreature != null)
                        modifiedCreature.Power += buffValue;
                    break;
                case CreatureTarget.LeftMost:
                    modifiedCreature = creatureController.AttackZone.Creatures.First();
                    if (modifiedCreature != null)
                        modifiedCreature.Power += buffValue;
                    break;
                case CreatureTarget.RightMost:
                    modifiedCreature = creatureController.AttackZone.Creatures.Last();
                    if (modifiedCreature != null)
                        modifiedCreature.Power += buffValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Serializable]
        public enum CreatureTarget
        {
            Weakest,
            Strongest,
            LeftMost,
            RightMost
        }
    }
}