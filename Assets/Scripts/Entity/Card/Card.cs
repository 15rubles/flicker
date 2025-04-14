using Entity.Card.Ability;
using UnityEngine;

namespace Entity.Card
{
    [CreateAssetMenu(fileName = "Card", menuName = "SOs/Card", order = 1)]
    public class Card : ScriptableObject
    {
        public CardName cardName;
        public CardStats cardStats;
        [SerializeReference]
        public CardAbility CardAbility;
        public CardCost cardCost;
        public Sprite visual;
    }
}