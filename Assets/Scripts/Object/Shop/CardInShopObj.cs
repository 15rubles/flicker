using Controller;
using Object.Deck;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Shop
{
    public class CardInShopObj : CardVisualObj, IPointerClickHandler
    {
        [SerializeField] private GameObject buyButton;

        [SerializeField] private TextMeshProUGUI priceText;

        public void SetCard(Entity.Card.Card card)
        {
            Card = card;
            priceText.text = "$" + Card.cardCost;
        }
        
        public void BuyCard()
        {
            var gameController = ControllerLocator.GetService<GameController>();
            bool result = gameController.TryToBuy(Card.cardCost);

            if (result)
            {
                var deck = ControllerLocator.GetService<BattleController>().Deck;
                deck.AllCards.Add(Card);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Not Enough Money :(");
            }

        }


        public void OnPointerClick(PointerEventData eventData)
        {
            buyButton.SetActive(!buyButton.activeSelf);
        }
    }
}