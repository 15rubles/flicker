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

        [SerializeField]
        private Dictionary<GameObject, CreatureSlot> gOsToCreatureSlots = new Dictionary<GameObject, CreatureSlot>();

        public Dictionary<GameObject, CreatureSlot> GOsToCreatureSlots
        {
            get => gOsToCreatureSlots;
            set => gOsToCreatureSlots = value;
        }

        public List<CreatureObj> Creatures
        {
            get
            {
                List<CreatureObj> list = new List<CreatureObj>();
                int children = transform.childCount;

                for (int i = 0; i < children; i++)
                {
                    list.Add(gOsToCreatureSlots[transform.GetChild(i).gameObject].CreatureObj);
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
                    list.Add(gOsToCreatureSlots[transform.GetChild(i).gameObject]);
                }
                return list;
            }
        }
        

        public Dictionary<GameObject, CreatureObj> GOsToCreatureObjs
        {
            get
            {
                return gOsToCreatureSlots.ToDictionary(slot => slot.Key,
                    slot => slot.Value.CreatureObj);
            }
        }
        
        public List<GameObject> CreatureGOs
        {
            get
            {
                return gOsToCreatureSlots.Select(slot => slot.Key).ToList();
            }
        }
        
       

        public void AddCreature(CreatureSlot creatureSlot)
        {
            creatureSlot.transform.SetParent(transform);
            GOsToCreatureSlots.Add(creatureSlot.gameObject, creatureSlot);
        }

        public void RemoveCreature(CreatureSlot creatureSlot)
        {
            gOsToCreatureSlots.Remove(creatureSlot.gameObject);
        }
        
    }
}