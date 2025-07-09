using System;
using Controller;
using Utils;

namespace Entity.Item.Ability
{
    [Serializable]
    public class ChangeRerollCostAbility : ItemAbility
    {
        public int firstPaidRerollCost = 2;
        public int increaseRerollAmount = 1;

        private int baseFirstPaidRerollCost, baseIncreaseRerollAmount;
        public override ItemType ItemType => ItemType.Passive;
        public override void EnableItem()
        {
            var shopController = ControllerLocator.GetService<ShopController>();
            baseFirstPaidRerollCost = shopController.FirstPaidRerollPrice;
            baseIncreaseRerollAmount = shopController.RerollPriceIncreaseStep;
            shopController.FirstPaidRerollPrice = firstPaidRerollCost;
            shopController.RerollPriceIncreaseStep = increaseRerollAmount;
        }
        public override void DisableItem()
        {
            var shopController = ControllerLocator.GetService<ShopController>();
            shopController.FirstPaidRerollPrice = baseFirstPaidRerollCost;
            shopController.RerollPriceIncreaseStep = baseIncreaseRerollAmount;
        }
        public override void Trigger() {}
    }
}