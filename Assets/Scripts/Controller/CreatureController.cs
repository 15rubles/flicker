using System.Collections.Generic;
using Entity.Card;
using Entity.Card.Ability;
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
        private Dictionary<GameObject, CreatureSlot> gOsToCreatureSlots = new Dictionary<GameObject, CreatureSlot>();
        
        public AttackZoneController AttackZone => attackZone;
        
        [SerializeField] 
        private GameObject creatureSlotPrefab;
        
        public Dictionary<GameObject, CreatureSlot> GOsToCreatureSlots
        {
            get => gOsToCreatureSlots;
            set => gOsToCreatureSlots = value;
        }

        private CreatureSlot lastPlayedCardSlot;

        public CreatureSlot LastPlayedCardSlot => lastPlayedCardSlot;


        public void SpawnCreature(Card cardData)
        {
            if (cardData.power != 0)
            {
                var creature = Instantiate(creatureSlotPrefab, attackZone.transform);
                var creatureSlot = creature.GetComponent<CreatureSlot>();
                lastPlayedCardSlot = creatureSlot;
                ChangeCreatureValuesAndEnable(creatureSlot, cardData, attackZone);
            }
            
            if (cardData.cardAbility.AbilityType == AbilityType.EnterTheBattlefield)
            {
                cardData.cardAbility.UseAbility(cardData);
            }
        }
        
        private void ChangeCreatureValuesAndEnable(CreatureSlot creatureSlot, Card cardData, ZoneController zone)
        {
            var creatureObj = creatureSlot.CreatureObj;
            creatureObj.Card = cardData;
            creatureObj.AttackCardZone = attackZone;
            AddCreature(zone, creatureSlot);
            creatureObj.UpdateText();
        }

        private void AddCreature(ZoneController zone, CreatureSlot creatureSlot)
        {
            creatureSlot.transform.SetParent(zone.transform);
            GOsToCreatureSlots.Add(creatureSlot.gameObject, creatureSlot);
        }
        

        public void ResetZones()
        {
            foreach (var creature in attackZone.CreatureSlots)
            {
                Destroy(creature.gameObject);
            }
            
            GOsToCreatureSlots = new Dictionary<GameObject, CreatureSlot>();
        }
    }
}