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
                                            .FindAll(creature => creature.Card.type == creatureType)
                                            .Count;
            creatureObj.Power = card.power + powerUp;
            return this;
        }
    }
}