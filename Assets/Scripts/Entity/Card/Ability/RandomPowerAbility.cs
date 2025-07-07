using System;
using Controller;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Entity.Card.Ability
{
    [Serializable]
    public class RandomPowerAbility : CardAbility
    {
        [SerializeField] private int minPower;
        [SerializeField] private int maxPower;
        
        public override CardAbility UseAbility(Card card)
        {
            var creatureObj = ControllerLocator.GetService<CreatureController>().LastPlayedCardSlot.CreatureObj;
            creatureObj.Power = Random.Range(minPower, maxPower + 1);
            return this;
        }
    }
}