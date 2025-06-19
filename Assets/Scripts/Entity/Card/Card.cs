using System.Collections.Generic;
using Entity.Card.Ability;
using Entity.Card.Attack;
using UnityEngine;

namespace Entity.Card
{
    [CreateAssetMenu(fileName = "Card", menuName = "SOs/Card", order = 1)]
    public class Card : ScriptableObject
    {
        public CardName cardName;
        public int power;
        [SerializeField] public List<KeywordType> keywords = new List<KeywordType>();
        [SerializeReference] public CardAbility cardAbility;
        [SerializeField] public List<CardType> cardTypes = new List<CardType>();
        [SerializeReference] public CardAttack cardAttack = new DefaultAttack();
        public CardCost cardCost;
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