using System;
using Controller;
using UnityEngine;
using Utils;

namespace Entity.Item.Ability
{
    [Serializable]
    public class PassiveIncreaseHandSizeAbility : ItemAbility
    {

        [SerializeField] private int increaseValue;
        
        public override ItemType ItemType => ItemType.Passive;
        public override void EnableItem()
        {
            ControllerLocator.GetService<BattleController>().StartHandCount += increaseValue;
        }
        public override void DisableItem()
        {
            ControllerLocator.GetService<BattleController>().StartHandCount -= increaseValue;
        }
        public override void Trigger()
        {}
    }
}