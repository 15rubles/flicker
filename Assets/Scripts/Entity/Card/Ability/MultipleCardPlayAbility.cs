using System;

namespace Entity.Card.Ability
{
    [Serializable]
    public class MultipleCardPlayAbility : CardAbility
    {
        public int playsLeft = 1;
        public override CardAbility UseAbility(Card card)
        {
            playsLeft--;
            IsNeededToBeDeleted = playsLeft <= 0;
            return this;
        }
    }
}