using Entity.Card;
using UnityEngine;
using TMPro;

namespace Object
{
    public class CardObject: MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI toughness;
        [SerializeField]
        private TextMeshProUGUI power;
        [SerializeField]
        private TextMeshProUGUI cardName;
        
        [SerializeField]
        private Card card;

        public Card Card
        {
            get => card;
            set => card = value;
        }

        void OnEnable()
        {
            cardName.text = card.cardName.ToString();
            toughness.text = card.cardStats.Toughness.ToString();
            power.text = card.cardStats.Power.ToString();
        }
    }
}