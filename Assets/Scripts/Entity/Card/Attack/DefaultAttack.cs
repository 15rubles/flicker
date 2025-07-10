using System.Threading.Tasks;
using Controller;
using Object.Creature;
using Object.Monster;
using UnityEngine;
using Utils;

namespace Entity.Card.Attack
{
    public class DefaultAttack : CardAttack
    {
        //return true if monster died
        public override async Task<bool> Attack(CreatureObj creature, MonsterObject monster)
        {
            MonsterController monsterController = ControllerLocator.GetService<MonsterController>();
            
            if (creature.Card.CheckKeyword(KeywordType.Trample))
            {
                Transform parent = monster.Slot.transform.parent;
                int power = creature.Power;
                bool isMonsterDied = false;
                
                while (power > 0)
                {
                    if (parent.childCount > 0)
                    {
                        GameObject targetSlot = parent.GetChild(0).gameObject;
                        var monsterObj = monsterController.GOsToMonsterSlots[targetSlot].MonsterObj;
                        monsterObj.Power -= power;
                        if (monsterObj.Power <= 0 || (creature.Card.CheckKeyword(KeywordType.Poison) && monsterObj.LastDamage > 0))
                        {
                            await monsterObj.DestroyMonster();
                            await Task.Yield();
                            isMonsterDied = true;
                        }
                        else
                        {
                            monsterObj.UpdateText();
                        }
                        //TODO poison dont work with trample and reduced damage
                        power = creature.Card.CheckKeyword(KeywordType.Poison) ? power - 1 : power - monsterObj.Power;
                    }
                    else
                    {
                        break;
                    }
                }
                return isMonsterDied;
            }
            monster.Power -= creature.Power;
            if (monster.Power <= 0 || (creature.Card.CheckKeyword(KeywordType.Poison) && monster.LastDamage > 0))
            {
                await monster.DestroyMonster();
                await Task.Yield();
                return true;
            }
            monster.UpdateText();
            return false;
        }

    }
}