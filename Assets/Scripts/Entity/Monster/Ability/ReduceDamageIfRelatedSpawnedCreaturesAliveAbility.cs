using System;
using Entity.Card.Ability;
using Object.Monster;
using UnityEngine;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class ReduceDamageIfRelatedSpawnedCreaturesAliveAbility : MonsterAbility
    {
        [Header("type should be etb to work properly")]
        [Header("spawnMonster should be etb type")]
        [SerializeField] private SpawnMonsterAbility spawnMonsterAbility;
        [SerializeField] private int damageReductionAmount = 2;
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            if (AbilityType == AbilityType.EnterTheBattlefield)
            {
                spawnMonsterAbility.UseAbility(monsterObject);
                AbilityType = AbilityType.DamageDealt;
            }
            
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