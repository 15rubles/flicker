using Controller;
using Entity.Item;
using TMPro;
using UnityEngine;
using Utils;

namespace Object.Shop
{
    public class ItemObj : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI priceText;
        
        [SerializeField]
        private TextMeshProUGUI nameText;
        
        //TODO dectiption obj popup

        [SerializeField] private ItemSO item;

        public void SetItem(ItemSO itemSo)
        {
            item = itemSo;
            priceText.text = "$" + item.price;
        }
        
        public void BuyItem()
        {
            var gameController = ControllerLocator.GetService<GameController>();
            bool result = gameController.TryToBuy(item.price);

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