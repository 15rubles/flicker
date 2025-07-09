using System;
using Controller;
using UnityEngine;
using Utils;

namespace Entity.Item.Ability
{
    [Serializable]
    public class PassiveIncreaseBaseShieldValueAbility : ItemAbility
    {
        public override ItemType ItemType => ItemType.Passive;
        
        [SerializeField] private int increaseValue;
        public override void EnableItem()
        {
            ControllerLocator.GetService<BattleController>().Deck.ShieldValue += increaseValue;
        }
        public override void DisableItem()
        {
            ControllerLocator.GetService<BattleController>().Deck.ShieldValue += increaseValue;
        }
        public override void Trigger() {}
    }
}