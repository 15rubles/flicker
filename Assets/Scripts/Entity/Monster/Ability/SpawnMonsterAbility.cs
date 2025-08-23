using System;
using System.Collections.Generic;
using Controller;
using Object.Monster;
using UnityEngine;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class SpawnMonsterAbility : MonsterAbility
    {
        [SerializeField] private List<Monster> monstersToSpawn = new List<Monster>();

        private List<MonsterSlot> spawnedMonsters = new List<MonsterSlot>();

        public List<MonsterSlot> SpawnedMonsters
        {
            get
            {
                spawnedMonsters.RemoveAll(item => item == null);
                return spawnedMonsters;
            }
        }
        public override MonsterAbility UseAbility(MonsterObject monsterObject)
        {
            MonsterController monsterController = ControllerLocator.GetService<MonsterController>();
            foreach (var monster in monstersToSpawn)
            {
                spawnedMonsters.Add(monsterController.SpawnMonsterAfter(monster, monsterObject.Slot.transform));
            }
            return this;
        }
    }
}