using System;
using Controller;
using Object.Card;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public class DrawCardAbility : CardAbility
    {

        public override CardAbility UseAbility(CardObject cardObj)
        {
            ControllerLocator.GetService<BattleController>().DealCards(1);
            return this;
        }
    }
}