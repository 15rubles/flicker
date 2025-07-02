using Controller;
using Utils;

namespace Entity.Card.ShieldAbility
{
    public class ExtraShieldValueAbility : ShieldCardAbility
    {
        public int extraShieldValue;
        
        public override void UpdateShieldValue(Card card)
        {
            ControllerLocator.GetService<BattleController>().UpdateShieldValue(card.power + extraShieldValue);
        }
    }
}