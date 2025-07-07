using System;
using Controller;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public class UndyingAbility : CardAbility
    {

        public override CardAbility UseAbility(Card card)
        {
            var battleController = ControllerLocator.GetService<BattleController>();
            
            battleController.ExtraCardsAtTheStartOfTheRound.Add(card);
            return this;
        }
    }
}