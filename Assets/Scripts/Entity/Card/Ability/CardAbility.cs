using System;

namespace Entity.Card.Ability
{
    [Serializable]
    public abstract class CardAbility
    {
        public abstract CardAbility UseAbility();
    }
}