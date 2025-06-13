using System;
using Object.Card;
using UnityEngine;

namespace Entity.Card.Ability
{
    [Serializable]
    public class DefaultAbility : CardAbility
    {
        public override CardAbility UseAbility(CardObject cardObject)
        {
            Debug.Log("ability");
            return this;
        }
    }
}