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
        
        public List<CreatureSlotViewInfo> CreatureSlotsViewInfos
        {
            get
            { 
                return creatureSlots.Select((slot, index) =>
                    new CreatureSlotViewInfo(
                        slot.gameObject.GetComponent<RectTransform>(), 
                        this, 
                        index
                    )
                ).ToList();
            }
        }

        public void AddCreature(CreatureSlot creatureSlot)
        {
            creatureSlot.transform.SetParent(transform);
            creatureSlots.Add(creatureSlot);
        }

        public void RemoveCreature(CreatureSlot creatureSlot)
        {
            creatureSlots.Remove(creatureSlot);
        }
        
    }
}