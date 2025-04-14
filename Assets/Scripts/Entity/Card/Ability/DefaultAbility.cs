using System;

namespace Entity.Card.Ability
{
    [Serializable]
    public class DefaultAbility : CardAbility
    {
        public override CardAbility UseAbility()
        {
            return this;
        }
    }
}