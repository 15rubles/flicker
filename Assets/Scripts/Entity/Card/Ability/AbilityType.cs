﻿using System;
using System.Collections.Generic;

namespace Entity.Card.Ability
{
    [Serializable]
    public enum AbilityType
    {
        EnterTheBattlefield,
        Aura,
        DeathRattle,
        EndOfCombat,
        BeginningOfCombat,
        DamageDealt
    }
    
    public static class AbilityTypeData
    {
        private readonly static Dictionary<AbilityType, string> Descriptions = new Dictionary<AbilityType, string>
        {
            { AbilityType.EnterTheBattlefield, "Enter the Battlefield"},
            { AbilityType.Aura, "Aura"},
            { AbilityType.DeathRattle, "Death Rattle"},
            { AbilityType.EndOfCombat, "End of Combat"},
            { AbilityType.BeginningOfCombat, "Beginning of Combat"},
            { AbilityType.DamageDealt, "Damage Dealt"},
        };

        public static string GetDescription(this AbilityType type)
        {
            return Descriptions.GetValueOrDefault(type, "Unknown");
        }
    }
}