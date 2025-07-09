using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entity.Card.Ability;
using Entity.Monster;
using Object.Monster;
using UnityEngine;
using Utils;

namespace Controller
{
    public class MonsterController: RegisteredMonoBehaviour
    {

        [SerializeField]
        private Dictionary<GameObject, MonsterSlot> gOsToMonsterSlots = new Dictionary<GameObject, MonsterSlot>();
        
        public List<MonsterSlot> MonsterSlots => monsterZoneController.MonsterSlots;

        public Dictionary<GameObject, MonsterSlot> GOsToMonsterSlots
        {
            get => gOsToMonsterSlots;
            set => gOsToMonsterSlots = value;
        }

        public List<MonsterObject> MonstersPool => monsterZoneController.Monsters;

        [SerializeField] 
        private GameObject monsterSlotPrefab;

        [SerializeField] private MonsterZoneController monsterZoneController;

        public void RemoveSlotFromMonsterSlots(MonsterSlot monsterSlot)
        {
            MonsterSlots.Remove(monsterSlot);
        }
        
        public void SpawnMonster(Monster monsterData)
        {
            var newMonster = Instantiate(monsterSlotPrefab, monsterZoneController.transform);
            var newMonsterSlot = newMonster.GetComponent<MonsterSlot>();
            ChangeCardValuesAndEnable(newMonsterSlot, monsterData);
        }

        private void ChangeCardValuesAndEnable(MonsterSlot monsterSlot, Monster monsterData)
        {
            var monster = monsterSlot.MonsterObj;
            monsterSlot.transform.SetParent(monsterZoneController.transform);
            monster.Monster = Instantiate(monsterData);
            monster.MonsterController = this;
            monster.UpdateText();
            GOsToMonsterSlots.Add(monsterSlot.gameObject, monsterSlot);
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
            var list = MonsterSlots;
            foreach (var monster in list)
            {
                Destroy(monster.gameObject);
            }
            GOsToMonsterSlots = new Dictionary<GameObject, MonsterSlot>();
        }

        public void EndOfCombatTrigger()
        {
            foreach (var triggerMonster in MonstersPool
                         .FindAll(monster => monster.Monster.Ability.AbilityType == AbilityType.EndOfCombat))
            {
                triggerMonster.Monster.Ability.UseAbility();
            }
        }
        
        public void BeginningOfCombatTrigger()
        {
            foreach (var triggerMonster in MonstersPool
                         .FindAll(monster => monster.Monster.Ability.AbilityType == AbilityType.EndOfCombat))
            {
                triggerMonster.Monster.Ability.UseAbility();
            }
        }
    }
}