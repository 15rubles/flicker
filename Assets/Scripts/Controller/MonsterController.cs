using System.Collections.Generic;
using System.Linq;
using Entity.Monster;
using Object;
using UnityEngine;

namespace Controller
{
    public class MonsterController: MonoBehaviour
    {
        [SerializeField] 
        private List<MonsterObject> monstersPool;

        public List<MonsterObject> MonstersPool => monstersPool;

        [SerializeField] 
        private GameObject monsterObjectPrefab;

        [SerializeField] private MonsterZoneController monsterZoneController;

        public void SpawnMonster(Monster monsterData)
        {
            var firstDisabled = monstersPool
                                .FirstOrDefault(monster => !monster.gameObject.activeInHierarchy);
            if (firstDisabled is not null)
            {
                ChangeCardValuesAndEnable(firstDisabled, monsterData);
            }
            else
            {
                var newMonster = Instantiate(monsterObjectPrefab, gameObject.transform.parent);
                newMonster.SetActive(false);
                var newMonsterObject = newMonster.GetComponent<MonsterObject>();
                monstersPool.Add(newMonsterObject);
                ChangeCardValuesAndEnable(newMonsterObject, monsterData);
            }
        }

        private void ChangeCardValuesAndEnable(MonsterObject monster, Monster monsterData)
        {
            monster.Monster = monsterData;
            monster.gameObject.SetActive(true);
            monster.transform.SetParent(monsterZoneController.transform);
            monster.UpdateText();
        }
    }
}