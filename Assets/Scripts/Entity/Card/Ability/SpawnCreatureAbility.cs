using System;
using System.Collections.Generic;
using Controller;
using UnityEngine;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public class SpawnCreatureAbility : CardAbility
    {
        [SerializeField] private List<Card> creaturesToSpawn = new List<Card>();

        public List<Card> CreaturesToSpawn => creaturesToSpawn;
        public override CardAbility UseAbility(Card cardObj)
        {
            CreatureController creatureController = ControllerLocator.GetService<CreatureController>();
            foreach (var creature in creaturesToSpawn)
            {
                creatureController.SpawnCreature(creature);
            }
            return this;
        }
    }
}