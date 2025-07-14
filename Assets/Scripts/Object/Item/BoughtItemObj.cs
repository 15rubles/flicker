using System;
using Controller;
using Entity.Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Item
{
    public class BoughtItemObj : ItemVisualObj, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI sellPriceText;
        [SerializeField] private GameObject sellButton;
        private int sellPrice;

        public new void SetItem(ItemSO itemSo)
        {
            base.SetItem(itemSo);
            sellPrice = Convert.ToInt32(Constants.SELL_PERCENTAGE * itemSo.Price);
            sellPriceText.text = sellPrice.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            sellButton.SetActive(!sellButton.activeInHierarchy);
        }

        public void SellItem()
        {
            var gameController = ControllerLocator.GetService<GameController>();
            gameController.SellItem(item, sellPrice);
            Destroy(gameObject);
        }
    }
}