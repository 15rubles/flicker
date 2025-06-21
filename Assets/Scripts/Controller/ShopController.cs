using System.Collections.Generic;
using Entity.Card;
using Object.Shop;
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

        override protected void Awake()
        {
            base.Awake();
            PrepareShop();
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
                cardInShop.Card = allCards[randomIndex];
            }
            
        }
    }
}