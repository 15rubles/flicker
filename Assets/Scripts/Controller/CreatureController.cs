using System.Collections.Generic;
using Entity.Card;
using Object;
using Object.Creature;
using UnityEngine;
using Utils;

namespace Controller
{
    public class CreatureController : RegisteredMonoBehaviour
    {
        [SerializeField] 
        private AttackZoneController attackZone;
        [SerializeField] 
        private BlockZoneController blockZone;
        
        [SerializeField]
        private Dictionary<GameObject, CreatureSlot> gOsToCreatureSlots = new Dictionary<GameObject, CreatureSlot>();
        
        public AttackZoneController AttackZone => attackZone;

        public BlockZoneController BlockZone => blockZone;
        
        [SerializeField] 
        private GameObject creatureSlotPrefab;
        
        public Dictionary<GameObject, CreatureSlot> GOsToCreatureSlots
        {
            get => gOsToCreatureSlots;
            set => gOsToCreatureSlots = value;
        }


        public void SpawnCreature(ZoneController zone, Card cardData)
        {
            var creature = Instantiate(creatureSlotPrefab, gameObject.transform.parent);
            var creatureSlot = creature.GetComponent<CreatureSlot>();
            ChangeCreatureValuesAndEnable(creatureSlot, cardData, zone);
        }
        
        private void ChangeCreatureValuesAndEnable(CreatureSlot creatureSlot, Card cardData, ZoneController zone)
        {
            var creatureObj = creatureSlot.CreatureObj;
            creatureObj.Card = cardData;
            creatureObj.AttackCardZone = attackZone;
            creatureObj.BlockCardZone = blockZone;
            AddCreature(zone, creatureSlot);
            creatureObj.UpdateText();
        }

        private void AddCreature(ZoneController zone, CreatureSlot creatureSlot)
        {
            creatureSlot.transform.SetParent(zone.transform);
            GOsToCreatureSlots.Add(creatureSlot.gameObject, creatureSlot);
        }
        
        public void SpawnHealthCard(Card healthCard)
        {
            SpawnCreature(blockZone, healthCard);
        }

        public void ResetZones()
        {
            foreach (var creature in attackZone.CreatureSlots)
            {
                Destroy(creature.gameObject);
            }
            foreach (var creature in blockZone.CreatureSlots)
            {
                Destroy(creature.gameObject);
            }
            
            GOsToCreatureSlots = new Dictionary<GameObject, CreatureSlot>();
        }
    }
}