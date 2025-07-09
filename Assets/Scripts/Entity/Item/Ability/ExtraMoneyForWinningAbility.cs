using System;
using Controller;
using Utils;

namespace Entity.Item.Ability
{
    [Serializable]
    public class ExtraMoneyForWinningAbility : ItemAbility
    {
        public int extraMoney = 1;
        public int turnThreshold = 1;
        public override ItemType ItemType => ItemType.BattleWon;
        public override void EnableItem() {}
        public override void DisableItem() {}
        public override void Trigger()
        {
            var battleController = ControllerLocator.GetService<BattleController>();
            
            if (Int32.Parse(battleController.TurnText.text) <= turnThreshold)
            {
                ControllerLocator.GetService<GameController>().Money += extraMoney;
            }
        }
    }
}