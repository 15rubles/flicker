using System;
using Entity.Card.Ability;
using UnityEngine;

namespace Entity.Monster.Ability
{
    [Serializable]
    public abstract class MonsterAbility
    {
        [SerializeField] private AbilityType abilityType;
        
        [SerializeField] private string description = "!!description is undefined!!";
        
        public AbilityType AbilityType => abilityType;
        public string Description => description;
        
        public abstract MonsterAbility UseAbility();
    }
}