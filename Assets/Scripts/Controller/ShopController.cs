using System.Collections.Generic;
using Entity.Card;
using Object.Shop;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Controller
{
    public class ShopController : RegisteredMonoBehaviour
    {
        [SerializeField] private GameObject cardsGrid;
        [SerializeField] private List<Card> allCards;
        [SerializeField] private GameObject cardInShopPrefab;
        [SerializeField] private BattleController battleController;
        
        [Required]
        [SerializeField] private GameController gameController;

        [Required]
        [SerializeField] private FullDeckForRemoveObj fullDeckForRemoveObj;

        [SerializeField] private int removeCardPrice = 5;

        override protected void Awake()
        {
            base.Awake();
            PrepareShop();
            gameController.UpdateMoneyText();
        }

        public void PrepareShop()
        {
            PrepareCardsForSale();
        }

        public void PrepareCardsForSale()
        {
            for (int i = 0; i < 6; i++)
            {
                var cardInShop = Instantiate(cardInShopPrefab, cardsGrid.transform).GetComponent<CardInShopObj>();
                int randomIndex = Random.Range(0, allCards.Count);
                cardInShop.SetCard(allCards[randomIndex]);
            }
            
        }

        public void RemoveCard()
        {
            bool result = gameController.TryToBuy(removeCardPrice);

            if (result)
            {
                fullDeckForRemoveObj.TurnOn();
            }
        }
    }
}