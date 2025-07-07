using System;
using Controller;
using UnityEngine;
using Utils;

namespace Entity.Card.ShieldAbility
{
    [Serializable]
    public class ExtraShieldValueAbility : ShieldCardAbility
    {
        [SerializeField] private int extraShieldValue;
        
        public override void UpdateShieldValue(Card card)
        {
            ControllerLocator.GetService<BattleController>().UpdateShieldValue(card.power + extraShieldValue);
        }
    }
}