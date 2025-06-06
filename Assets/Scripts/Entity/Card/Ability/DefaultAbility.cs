using System;
using UnityEngine;

namespace Entity.Card.Ability
{
    [Serializable]
    public class DefaultAbility : CardAbility
    {
        public override CardAbility UseAbility(Card card)
        {
            Debug.Log("ability");
            return this;
        }
    }
}