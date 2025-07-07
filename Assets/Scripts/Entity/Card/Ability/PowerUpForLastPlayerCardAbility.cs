using Controller;
using UnityEngine;
using Utils;

namespace Entity.Card.Ability
{
    public class PowerUpForLastPlayerCardAbility : CardAbility
    {
        [SerializeField] private int powerUpValue = 1;

        public override CardAbility UseAbility(Card card)
        {
            var lastPlayedCreature = ControllerLocator.GetService<CreatureController>().LastPlayedCardSlot.CreatureObj;
            lastPlayedCreature.Power += powerUpValue;
            return this;
        }
    }
}