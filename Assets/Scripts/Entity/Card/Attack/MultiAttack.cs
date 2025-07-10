using System;
using System.Threading.Tasks;
using Controller;
using Object.Creature;
using Object.Monster;
using UnityEngine;
using Utils;

namespace Entity.Card.Attack
{
    [Serializable]
    public class MultiAttack : CardAttack
    {
        public int attackQuantity = 2;

        public override async Task<bool> Attack(CreatureObj creature, MonsterObject monster)
        {
            MonsterController monsterController = ControllerLocator.GetService<MonsterController>();

            Transform parent = monster.Slot.transform.parent;
            bool isMonsterDied = false;

            for (int i = 0; i < attackQuantity; i++)
            {
                if (parent.childCount > 0)
                {
                    GameObject targetSlot = parent.GetChild(0).gameObject;
                    var monsterObj = monsterController.GOsToMonsterSlots[targetSlot].MonsterObj;
                    monsterObj.Power -= creature.Power;
                    if (monsterObj.Power <= 0 || creature.Card.CheckKeyword(KeywordType.Poison) && monsterObj.LastDamage > 0)
                    {
                        await monsterObj.DestroyMonster();
                        await Task.Yield();
                        isMonsterDied = true;
                    }
                    else
                    {
                        monsterObj.UpdateText();
                    }
                }
                else
                {
                    break;
                }
            }
            return isMonsterDied;
            
        }
    }
}