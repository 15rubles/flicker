using System.Collections.Generic;
using System.Linq;
using Object.Creature;
using UnityEngine;
using Utils;

namespace Controller
{
    public class ZoneController : RegisteredMonoBehaviour
    {
        public RectTransform rectTransform;
        public CreatureController creatureController;

        public Dictionary<GameObject, CreatureSlot> GOsToCreatureSlots => creatureController.GOsToCreatureSlots;

        public List<CreatureObj> Creatures
        {
            get
            {
                List<CreatureObj> list = new List<CreatureObj>();
                int children = transform.childCount;

                for (int i = 0; i < children; i++)
                {
                    list.Add(GOsToCreatureSlots[transform.GetChild(i).gameObject].CreatureObj);
                }
                return list;
            }
        }
        
        public List<CreatureSlot> CreatureSlots
        {
            get
            {
                List<CreatureSlot> list = new List<CreatureSlot>();
                int children = transform.childCount;

                for (int i = 0; i < children; i++)
                {
                    list.Add(GOsToCreatureSlots[transform.GetChild(i).gameObject]);
                }
                return list;
            }
        }

        public Dictionary<GameObject, CreatureObj> GOsToCreatureObjs
        {
            get
            {
                return GOsToCreatureSlots.ToDictionary(slot => slot.Key,
                    slot => slot.Value.CreatureObj);
            }
        }
        
        public List<GameObject> CreatureGOs
        {
            get
            {
                return GOsToCreatureSlots.Select(slot => slot.Key).ToList();
            }
        }
        
    }
}