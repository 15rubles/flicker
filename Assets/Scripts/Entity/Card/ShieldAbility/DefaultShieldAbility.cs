using System;
using Controller;
using Utils;

namespace Entity.Card.ShieldAbility
{
    [Serializable]
    public class DefaultShieldAbility : ShieldCardAbility
    {
        public override void UpdateShieldValue(Card card)
        {
            ControllerLocator.GetService<BattleController>().UpdateShieldValue(card.power);
        }
    }
}