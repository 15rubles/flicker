using Controller;
using Entity.Card;
using TMPro;
using UnityEngine;

namespace Object
{
    public class CreatureObj : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI power;
        [SerializeField] private TextMeshProUGUI cardName;
        
        public Card Card { get; set; }
        
        public ZoneController Zone { get; set; }

        public void UpdateText()
        {
            power.text = Card.cardStats.Power.ToString();
            cardName.text = Card.cardName.ToString();
        }
    }
}