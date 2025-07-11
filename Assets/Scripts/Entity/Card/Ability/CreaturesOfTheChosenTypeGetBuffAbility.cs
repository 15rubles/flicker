﻿using System;
using System.Linq;
using Controller;
using Object.Card;
using Object.Creature;
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
            foreach (CreatureObj creature in creatureController.AttackZone.Creatures
                                                               .Where(creature => creature.Card.cardTypes.Contains(cardType)))
            {
                creature.Power = creature.Card.power + buffValue;
                creature.UpdateText();
            }
            return this;
        }
    }
}