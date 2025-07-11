using System;
using Controller;
using Utils;

namespace Entity.Card.ShieldAbility
{
    [Serializable]
    public class PowerBonusForEachCreatureOfChosenTypeShieldAbility : ShieldCardAbility
    {
        
        public CardType creatureType;
        public override void UpdateShieldValue(Card card)
        {
            var creatureController = ControllerLocator.GetService<CreatureController>();
            int powerUp = creatureController.AttackZone.Creatures
                                            .FindAll(creature => creature.Card.cardTypes.Contains(creatureType))
                                            .Count;
            ControllerLocator.GetService<BattleController>().UpdateShieldValue(card.power + powerUp);
        }
    }
}