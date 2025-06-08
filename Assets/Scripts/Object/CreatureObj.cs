using Controller;
using Entity.Card;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Object
{
    public class CreatureObj : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private TextMeshProUGUI cardName;

        public int Power { get; set; }

        private Card card;
        
        public Card Card
        {
            get => card;
            set
            {
                card = value;
                Power = value.power;
            }
        }

        public ZoneController Zone { get; set; }

        public void UpdateText()
        {
            powerText.text = Power.ToString();
            cardName.text = Card.cardName.ToString();
        }
    }
}