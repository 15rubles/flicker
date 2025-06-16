using System;
using System.Collections.Generic;

namespace Entity.Card.Ability
{
    [Serializable]
    public enum KeywordType
    {
        Poison,
        Attacker,
        Blocker,
        Untouchable
    }
    
    public static class KeywordTypeData
    {
        private readonly static Dictionary<KeywordType, string> Descriptions = new Dictionary<KeywordType, string>
        {
            { KeywordType.Untouchable, "Can't be rearranged" },
            { KeywordType.Poison, "Kill monster if deal any damage to it" },
            { KeywordType.Blocker, "Can only be played to block zone" },
            { KeywordType.Attacker, "Can only be played to attack zone" }
        };

        public static string GetDescription(this KeywordType type)
        {
            return Descriptions.GetValueOrDefault(type, "Unknown");
        }
    }
}