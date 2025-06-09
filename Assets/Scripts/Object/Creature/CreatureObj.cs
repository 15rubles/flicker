using Controller;
using TMPro;
using UnityEngine;

namespace Object.Creature
{
    public class CreatureObj : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI powerText;
        [SerializeField] private TextMeshProUGUI cardName;
        
        [SerializeField] private CreatureSlot slot;

        public CreatureSlot Slot
        {
            get => slot;
            set => slot = value;
        }
        public int Power { get; set; }

        private Entity.Card.Card card;
        
        public Entity.Card.Card Card
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