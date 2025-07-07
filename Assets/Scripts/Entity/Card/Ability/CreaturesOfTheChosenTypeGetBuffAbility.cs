using System;
using Controller;
using Object.Card;
using UnityEngine;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public class CreaturesOfTheChosenTypeGetBuffAbility : CardAbility
    {
        [SerializeField] private int buffValue;
        [SerializeField] private CardType cardType;
        
        public override CardAbility UseAbility(Card cardObj)
        {
            CreatureController creatureController = ControllerLocator.GetService<CreatureController>();
            foreach (var creature in creatureController.AttackZone.Creatures)
            {
                if (creature.Card.cardTypes.Contains(cardType))
                {
                    creature.Power += buffValue;
                    creature.UpdateText();
                }
            }
            return this;
        }
    }
}