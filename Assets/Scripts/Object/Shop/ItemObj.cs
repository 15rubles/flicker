using Controller;
using Entity.Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Object.Shop
{
    public class ItemObj : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI priceText;
        
        [SerializeField] private GameObject descriptionBox;
        [SerializeField] private TextMeshProUGUI descriptionText;
        
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private ItemSO item;

        public void SetItem(ItemSO itemSo)
        {
            item = Instantiate(itemSo);
            priceText.text = "$" + item.Price;
            nameText.text = item.itemName;
            descriptionText.text = item.Description;
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
        public void OnPointerEnter(PointerEventData eventData)
        {
            descriptionBox.SetActive(true);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            descriptionBox.SetActive(false);
        }
    }
}