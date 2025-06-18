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
        
        public AttackZoneController AttackZone => attackZone;

        public BlockZoneController BlockZone => blockZone;
        
        [SerializeField] 
        private GameObject creatureSlotPrefab;


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
            // creatureObj.Zone = zone;
            creatureObj.AttackCardZone = attackZone;
            creatureObj.BlockCardZone = blockZone;
            zone.AddCreature(creatureSlot);
            creatureObj.UpdateText();
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
            attackZone.GOsToCreatureSlots = new Dictionary<GameObject, CreatureSlot>();
            
            foreach (var creature in blockZone.CreatureSlots)
            {
                Destroy(creature.gameObject);
            }
            blockZone.GOsToCreatureSlots = new Dictionary<GameObject, CreatureSlot>();
        }
    }
}