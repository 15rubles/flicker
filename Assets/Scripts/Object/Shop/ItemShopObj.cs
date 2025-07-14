using Controller;
using Entity.Item;
using Object.Item;
using TMPro;
using UnityEngine;
using Utils;

namespace Object.Shop
{
    public class ItemShopObj : ItemVisualObj
    {
        [SerializeField] private TextMeshProUGUI priceText;

        public new void SetItem(ItemSO itemSo)
        {
            base.SetItem(itemSo);
            priceText.text = "$" + item.Price;
        }
        
        public void BuyItem()
        {
            var gameController = ControllerLocator.GetService<GameController>();
            bool result = gameController.TryToBuy(item.Price);

            if (result)
            {
                gameController.AddItem(item);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Not Enough Money :(");
            }

        }
    }
}