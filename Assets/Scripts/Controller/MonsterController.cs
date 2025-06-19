using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entity.Monster;
using Object.Monster;
using UnityEngine;
using Utils;

namespace Controller
{
    public class MonsterController: RegisteredMonoBehaviour
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

        public void RemoveSlotFromMonsterSlots(MonsterSlot monsterSlot)
        {
            monsterSlots.Remove(monsterSlot);
        }
        
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
            monster.MonsterController = this;
            monster.UpdateText();
            SpawnMonsterAnimation(monsterSlot.gameObject);
        }

        private void SpawnMonsterAnimation(GameObject monster)
        {
            RectTransform rectTransform = monster.GetComponent<RectTransform>();

            float duration = 0.4f;
            float overshoot = 1.2f;
            
            rectTransform.localScale = Vector3.zero;
            rectTransform.DOScale(Vector3.one, duration)
                         .SetEase(Ease.OutBack, overshoot);
        }

        public void Reset()
        {
            foreach (var monster in monsterSlots)
            {
                Destroy(monster);
            }
            monsterSlots = new List<MonsterSlot>();
        }
    }
}