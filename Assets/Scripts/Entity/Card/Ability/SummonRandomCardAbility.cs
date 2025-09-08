using System;
using System.Linq;
using Controller;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Entity.Card.Ability
{
    [Serializable]
    public class SummonRandomCardAbility : CardAbility
    {
        [SerializeField] private Rarity summonCardRarity = Rarity.Common;
        [SerializeField] private int quantityToSpawn = 1;
        
        public override CardAbility UseAbility(Card card)
        {
            var creatureController = ControllerLocator.GetService<CreatureController>();
            var shopController = ControllerLocator.GetService<ShopController>();

            var listOfCards = shopController.AllCards.Where(cardInList => cardInList.rarity == summonCardRarity)
                .Where(cardInList => cardInList.power > 0).ToList();
            for (int i = 0; i < quantityToSpawn; i++)
            {
                int randomIndex = Random.Range(0, listOfCards.Count);
                creatureController.SpawnCreature(listOfCards[randomIndex]);
            }
            return this;
        }
    }
}