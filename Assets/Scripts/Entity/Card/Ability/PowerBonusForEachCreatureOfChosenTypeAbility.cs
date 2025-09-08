using System;
using Controller;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public class PowerBonusForEachCreatureOfChosenTypeAbility : CardAbility
    {
        public CardType creatureType;
        public override CardAbility UseAbility(Card card)
        {
            var creatureController = ControllerLocator.GetService<CreatureController>();
            var creatureObj = creatureController.LastPlayedCardSlot.CreatureObj;
            int powerUp = creatureController.AttackZone.Creatures
                                            .Count;
            creatureObj.Power = card.power + powerUp - 1;
            return this;
        }
    }
}