using System;
using Object.Monster;
using UnityEngine;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class ReduceDamageIfRelatedSpawnedCreaturesAliveAbility : MonsterAbility
    {
        [Header("Work best with damageDealt trigger type")]
        [SerializeField] private SpawnMonsterAbility spawnMonsterAbility;
        [SerializeField] private int damageReductionAmount = 2;
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            if (spawnMonsterAbility.SpawnedMonsters.Count > 0)
            {
                monsterObject.Power =
                    monsterObject.LastDamage > damageReductionAmount
                        ? monsterObject.Power + damageReductionAmount
                        : monsterObject.PreviousPower;
            }
            return this;
        }
    }
}