using System;
using Controller;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Entity.Card.ShieldAbility
{
    [Serializable]
    public class RandomShieldValueAbility : ShieldCardAbility
    {
        [SerializeField] private int minPower;
        [SerializeField] private int maxPower;
        public override void UpdateShieldValue(Card card)
        {
            ControllerLocator.GetService<BattleController>().UpdateShieldValue(Random.Range(minPower, maxPower));
        }
    }
}