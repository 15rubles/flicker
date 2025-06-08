using System.Collections.Generic;
using Entity.Card;
using Object;
using UnityEngine;

namespace Controller
{
    public class CreatureController : MonoBehaviour
    {
        [SerializeField] 
        private AttackZoneController attackZone;
        [SerializeField] 
        private BlockZoneController blockZone;
        
        public AttackZoneController AttackZone => attackZone;

        public BlockZoneController BlockZone => blockZone;
        
        [SerializeField] 
        private GameObject creaturePrefab;


        public void SpawnCreature(ZoneController zone, Card cardData)
        {
            var creature = Instantiate(creaturePrefab, gameObject.transform.parent);
            var creatureObj = creature.GetComponent<CreatureObj>();
            ChangeCreatureValuesAndEnable(creatureObj, cardData, zone);
        }
        
        private void ChangeCreatureValuesAndEnable(CreatureObj creatureObj, Card cardData, ZoneController zone)
        {
            creatureObj.Card = cardData;
            creatureObj.Zone = zone;
            zone.AddCreature(creatureObj);
            creatureObj.UpdateText();
        }
        
        public void SpawnHealthCard(Card healthCard)
        {
            SpawnCreature(blockZone, healthCard);
        }

        public void ResetZones(Card healthCard)
        {
            foreach (var creature in attackZone.Creatures)
            {
                Destroy(creature.gameObject);
            }
            attackZone.Creatures = new List<CreatureObj>();
            
            foreach (var creature in blockZone.Creatures)
            {
                Destroy(creature.gameObject);
            }
            blockZone.Creatures = new List<CreatureObj>();
            
            SpawnHealthCard(healthCard);
        }
    }
}