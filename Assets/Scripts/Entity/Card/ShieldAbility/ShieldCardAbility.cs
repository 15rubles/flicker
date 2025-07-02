using System;

namespace Entity.Card.ShieldAbility
{
    [Serializable]
    public abstract class ShieldCardAbility
    {
        public abstract void UpdateShieldValue(Card card);
    }
}