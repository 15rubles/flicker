using System;
using UnityEngine;
using Utils;

namespace Entity.Card.Ability
{
    [Serializable]
    public abstract class CardAbility
    {
        [SerializeField] private AbilityType abilityType;
        
        [TextArea]
        [SerializeField]
        private string description = "!!description is undefined!!";
        
        public AbilityType AbilityType => abilityType;

        public string Description
        {
            get
            {
                if (abilityType == AbilityType.DescriptionNeeded)
                {
                    return description;
                }
                
                if (abilityType == AbilityType.None)
                {
                    return "";
                }
                return abilityType.GetDescription() + Constants.TRIGGER_TO_DESCRIPTION_CONNECTOR +  description;
            }
        }

        private bool isNeededToBeDeleted = true;
        
        public bool IsNeededToBeDeleted
        {
            get => isNeededToBeDeleted;
            set => isNeededToBeDeleted = value;
        }

        public abstract CardAbility UseAbility(Card card);
    }
}