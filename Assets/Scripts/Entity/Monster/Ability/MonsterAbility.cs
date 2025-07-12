using System;
using Entity.Card.Ability;
using Object.Monster;
using UnityEngine;
using Utils;

namespace Entity.Monster.Ability
{
    [Serializable]
    public abstract class MonsterAbility
    {
        [SerializeField] private AbilityType abilityType;
        
        [TextArea]
        [SerializeField] private string description = "!!description is undefined!!";

        public AbilityType AbilityType
        {
            get => abilityType;
            set => abilityType = value;
        }
        public string Description => abilityType.GetDescription() + Constants.TRIGGER_TO_DESCRIPTION_CONNECTOR + description;
        
        public abstract MonsterAbility UseAbility(MonsterObject monsterObject);
    }
}