using System;
using Object.Card;
using UnityEngine;

namespace Entity.Card.Ability
{
    [Serializable]
    public abstract class CardAbility
    {
        [SerializeField] private AbilityType abilityType;
        
        [SerializeField] private string description = "!!description is undefined!!";
        
        public AbilityType AbilityType => abilityType;
        public string Description => description;

        public abstract CardAbility UseAbility(CardObject cardObj);
    }
}