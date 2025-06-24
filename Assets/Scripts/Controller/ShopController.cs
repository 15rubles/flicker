using System.Collections.Generic;
using Entity.Card;
using Entity.Item;
using Object.Shop;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Controller
{
    public class ShopController : RegisteredMonoBehaviour
    {
        [SerializeField] private GameObject cardsGrid;
        [SerializeField] private GameObject itemsGrid;
        [SerializeField] private List<Card> allCards;
        [SerializeField] private List<ItemSO> allItems;
        [SerializeField] private GameObject cardInShopPrefab;
        [SerializeField] private GameObject itemInShopPrefab;
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
            gameController.UpdateMoneyText();
            PrepareCardsForSale();
            PrepareItemsForSale();
        }

        public void PrepareCardsForSale()
        {
            foreach (Transform child in cardsGrid.transform)
            {
                Destroy(child.gameObject);
            }
            
            for (int i = 0; i < 6; i++)
            {
                var cardInShop = Instantiate(cardInShopPrefab, cardsGrid.transform).GetComponent<CardInShopObj>();
                int randomIndex = Random.Range(0, allCards.Count);
                cardInShop.SetCard(allCards[randomIndex]);
            }
            
        }
        
        public void PrepareItemsForSale()
        {
            foreach (Transform child in itemsGrid.transform)
            {
                Destroy(child.gameObject);
            }
            
            for (int i = 0; i < 3; i++)
            {
                var itemInShop = Instantiate(itemInShopPrefab, itemsGrid.transform).GetComponent<ItemObj>();
                int randomIndex = Random.Range(0, allItems.Count);
                itemInShop.SetItem(allItems[randomIndex]);
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

        public void CloseShop()
        {
            gameController.StartNewEncounter();
        }
    }
}