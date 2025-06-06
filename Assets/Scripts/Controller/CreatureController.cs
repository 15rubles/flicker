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
        
        [SerializeField] 
        private GameObject creaturePrefab;

        private List<CreatureObj> attackCreatures = new List<CreatureObj>();

        private List<CreatureObj> blockCreatures = new List<CreatureObj>();

        public void SpawnCreature(ZoneController zone, Card cardData)
        {
            var creature = Instantiate(creaturePrefab, gameObject.transform.parent);
            var creatureObj = creature.GetComponent<CreatureObj>();
            if (zone == attackZone)
            {
                Debug.Log("attack");
                attackCreatures.Add(creatureObj);
            }
            else
            {
                Debug.Log("block");
                blockCreatures.Add(creatureObj);
            }
            ChangeCreatureValuesAndEnable(creatureObj, cardData, zone);
        }
        
        private void ChangeCreatureValuesAndEnable(CreatureObj creatureObj, Card cardData, ZoneController zone)
        {
            creatureObj.Card = cardData;
            creatureObj.Zone = zone;
            zone.AddCreature(creatureObj);
            creatureObj.UpdateText();
        }
    }
}