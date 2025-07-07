using System;
using Controller;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public class CopyLastCardAbility : CardAbility
    {
        public override CardAbility UseAbility(Card card)
        {
            var lastCard = ControllerLocator.GetService<CreatureController>().LastPlayedCardSlot.CreatureObj.Card;

            if (lastCard.cardAbility.AbilityType == AbilityType.EnterTheBattlefield)
            {
                lastCard.cardAbility.UseAbility(lastCard);
            }
            return this;
        }
    }
}