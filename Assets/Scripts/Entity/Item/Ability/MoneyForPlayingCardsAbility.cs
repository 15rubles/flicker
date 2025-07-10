using System;
using Controller;
using Utils;

namespace Entity.Item.Ability
{
    [Serializable]
    public class MoneyForPlayingCardsAbility : ItemAbility
    {
        public int cardsThresholdToGetMoney = 10;
        public int moneyPerCardsThreshold = 1;
        
        private int counter = 0;
        
        public override ItemType ItemType => ItemType.TriggerCardPlayed;
        public override void EnableItem() {}
        public override void DisableItem() {}
        public override void Trigger()
        {
            counter++;
            if (counter < cardsThresholdToGetMoney) 
                return;

            counter = 0;
            ControllerLocator.GetService<GameController>().Money += moneyPerCardsThreshold;
        }
    }
}