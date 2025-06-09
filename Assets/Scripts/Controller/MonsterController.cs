using System.Collections.Generic;
using System.Linq;
using Entity.Monster;
using Object.Monster;
using UnityEngine;

namespace Controller
{
    public class MonsterController: MonoBehaviour
    {
        [SerializeField] 
        private List<MonsterSlot> monsterSlots;

        public List<MonsterSlot> MonsterSlots => monsterSlots;

        public List<MonsterObject> MonstersPool
        {
            get
            {
                return monsterSlots.Select(slot => slot.MonsterObj).ToList();
            }
        }

        [SerializeField] 
        private GameObject monsterSlotPrefab;

        [SerializeField] private MonsterZoneController monsterZoneController;

        public void SpawnMonster(Monster monsterData)
        {
            var firstDisabled = monsterSlots
                                .FirstOrDefault(monster => !monster.gameObject.activeInHierarchy);
            if (firstDisabled is not null)
            {
                ChangeCardValuesAndEnable(firstDisabled, monsterData);
            }
            else
            {
                var newMonster = Instantiate(monsterSlotPrefab, gameObject.transform.parent);
                var newMonsterSlot = newMonster.GetComponent<MonsterSlot>();
                monsterSlots.Add(newMonsterSlot);
                ChangeCardValuesAndEnable(newMonsterSlot, monsterData);
            }
        }

        private void ChangeCardValuesAndEnable(MonsterSlot monsterSlot, Monster monsterData)
        {
            var monster = monsterSlot.MonsterObj;
            monsterSlot.transform.SetParent(monsterZoneController.transform);
            monster.Monster = monsterData;
            monster.UpdateText();
        }
    }
}