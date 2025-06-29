using System.Collections.Generic;
using Entity.Card.Ability;
using Entity.Card.Attack;
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
        public int cardCost;
        public Sprite visual;
        public CardType type;

        public bool CheckKeyword(KeywordType keywordToCheck)
        {
            return keywords.Contains(keywordToCheck);
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