using System;
using System.Collections.Generic;
using Controller;
using UnityEngine;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public class SpawnMonsterAbility : MonsterAbility
    {
        [SerializeField] private List<Monster> monstersToSpawn = new List<Monster>();

        public List<Monster> MonstersToSpawn => monstersToSpawn;
        
        public override MonsterAbility UseAbility()
        {
            MonsterController monsterController = ControllerLocator.GetService<MonsterController>();
            foreach (var monster in monstersToSpawn)
            {
                monsterController.SpawnMonster(monster);
            }
            return this;
        }
    }
}