using Controller;
using Utils;

namespace Entity.Card.ShieldAbility
{
    public class DefaultShieldAbility : ShieldCardAbility
    {
        public override void UpdateShieldValue(Card card)
        {
            ControllerLocator.GetService<BattleController>().UpdateShieldValue(card.power);
        }
    }
}