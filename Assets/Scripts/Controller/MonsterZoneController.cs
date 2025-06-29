using System.Collections.Generic;
using Object.Monster;
using UnityEngine;

namespace Controller
{
    public class MonsterZoneController: MonoBehaviour
    {
        [SerializeField] private MonsterController monsterController;
        
        public List<MonsterObject> Monsters
        {
            get
            {
                List<MonsterObject> list = new List<MonsterObject>();
                int children = transform.childCount;

                for (int i = 0; i < children; i++)
                {
                    list.Add(monsterController.GOsToMonsterSlots[transform.GetChild(i).gameObject].MonsterObj);
                }
                return list;
            }
        }
        
        public List<MonsterSlot> MonsterSlots
        {
            get
            {
                List<MonsterSlot> list = new List<MonsterSlot>();
                int children = transform.childCount;

                for (int i = 0; i < children; i++)
                {
                    list.Add(monsterController.GOsToMonsterSlots[transform.GetChild(i).gameObject]);
                }
                return list;
            }
        }
    }
}