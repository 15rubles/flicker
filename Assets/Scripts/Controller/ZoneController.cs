using System.Collections.Generic;
using System.Linq;
using Object;
using Object.Creature;
using UnityEngine;

namespace Controller
{
    public class ZoneController : MonoBehaviour
    {
        public RectTransform rectTransform;
        public CreatureController creatureController;

        private List<CreatureSlot> creatureSlots = new List<CreatureSlot>();

        public List<CreatureSlot> CreatureSlots
        {
            get => creatureSlots;
            set => creatureSlots = value;
        }

        public List<CreatureObj> Creatures
        {
            get
            {
                return creatureSlots.Select(slot => slot.CreatureObj).ToList();
            }
        }

        public void AddCreature(CreatureSlot creatureSlot)
        {
            creatureSlot.transform.SetParent(transform);
            creatureSlots.Add(creatureSlot);
        }
        
    }
}