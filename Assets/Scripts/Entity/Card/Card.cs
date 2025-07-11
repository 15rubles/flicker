using System.Collections.Generic;
using Entity.Card.Ability;
using Entity.Card.Attack;
using Entity.Card.ShieldAbility;
using UnityEngine;

namespace Entity.Card
{
    // Spawn 1 solder - 1
    // Etb All solders get +1 - 1
    // Creatures to left and right gets poison - 1
    // Etb sac 2 creatures - 7
    // Etb trigger etb of other creature -1 
    // Solder that gives solder type to creature at the left and right - 3
    // Trample - 5
    // Etb target monster lose all abilities - 1
    // Solder \ deal damage to all monsters - 1
    // Etb creature +1 - 1
    
    [CreateAssetMenu(fileName = "Card", menuName = "SOs/Card", order = 1)]
    public class Card : ScriptableObject
    {
        public string cardName;
        [Header("if power is 0, card will be treated like spell")]
        public int power;
        [SerializeField] public List<KeywordType> keywords = new List<KeywordType>();
        [SerializeReference] public CardAbility cardAbility;
        [SerializeField] public List<CardType> cardTypes = new List<CardType>();
        [SerializeReference] public CardAttack cardAttack = new DefaultAttack();
        [SerializeReference] public ShieldCardAbility shieldCardAbility = new DefaultShieldAbility();
        
        [Header("If price == 0 will use default value")]
        public int cardCost = 0;
        public Sprite visual;
        public Rarity rarity = Rarity.Common;

        public bool CheckKeyword(KeywordType keywordToCheck)
        {
            return keywords.Contains(keywordToCheck);
        }

        public int Price
        {
            get
            {
                if (cardCost != 0)
                    return cardCost;
                switch (rarity)
                {
                    case Rarity.Common:
                        return 4;
                    case Rarity.Rare:
                        return 7;
                    case Rarity.UltraRare:
                        return 11;
                }
                return cardCost;
            }
        }

        public string Description
        {
            get
            {
                string keywordStr = "";
                foreach (var keyword in keywords)
                {
                    keywordStr += keyword.ToString() + '\n';
                }
                return keywordStr + '\n' + cardAbility.Description;
            }
        }
    }
}