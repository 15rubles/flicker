using System;
using System.Linq;
using System.Threading.Tasks;
using Controller;
using Entity.Monster;
using Object.Creature;
using Object.Monster;
using UnityEngine;
using Utils;

namespace Entity.Card.Attack
{
    [Serializable]
    public class KillMonsterOnAttack : CardAttack
    {
        [Header("Not working on bosses")]
        public MonsterTarget monsterTarget = MonsterTarget.Weakest;
        public override async Task<bool> Attack(CreatureObj creature, MonsterObject monster)
        {
            var monsters = ControllerLocator.GetService<MonsterController>().MonstersPool;
            
            switch (monsterTarget)
            {
                case MonsterTarget.Weakest:
                    var weakestMonster = monsters.FindAll(m => !m.Monster.MonsterTypes.Contains(MonsterType.Boss))
                                                 .OrderBy(monst => monst.Power).FirstOrDefault();
                    if (weakestMonster != null)
                    {
                        await weakestMonster.DestroyMonster();
                        await Task.Yield();
                    }
                    break;
                case MonsterTarget.Strongest:
                    var strongestMonster = monsters.FindAll(m => !m.Monster.MonsterTypes.Contains(MonsterType.Boss))
                                                   .OrderByDescending(monst => monst.Power).FirstOrDefault();
                    if (strongestMonster != null)
                    {
                        await strongestMonster.DestroyMonster();
                        await Task.Yield();
                    }
                    break;
            }

            var defaultAttack = new DefaultAttack();
            await defaultAttack.Attack(creature, monster);
            return true;
        }

        [Serializable]
        public enum MonsterTarget
        {
            Weakest,
            Strongest
        }
    }
}