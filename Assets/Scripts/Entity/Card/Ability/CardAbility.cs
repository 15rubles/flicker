using System;
using Object.Card;
using UnityEngine;

namespace Entity.Card.Ability
{
    [Serializable]
    public abstract class CardAbility
    {
        [SerializeField] private AbilityType abilityType;
        
        public AbilityType AbilityType => abilityType;

        public abstract CardAbility UseAbility(CardObject cardObj);
    }
}