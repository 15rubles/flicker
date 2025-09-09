using System;
using Entity.Card.Ability;
using Object.Monster;
using UnityEngine;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class ReduceDamageIfRelatedSpawnedCreaturesAliveAbility : MonsterAbility
    {
        [Header("spawnMonster should be etb type")]
        [SerializeField] private SpawnMonsterAbility spawnMonsterAbility;
        [SerializeField] private int damageReductionAmount = 1;
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            if (spawnMonsterAbility.SpawnedMonsters.Count > 0)
            {
                monsterObject.Power =
                    monsterObject.LastDamage > damageReductionAmount
                        ? monsterObject.Power + damageReductionAmount
                        : monsterObject.PreviousPower;
            }
            
            spawnMonsterAbility.UseAbility(monsterObject);
            return this;
        }
    }
}