using Entity.Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Object.Item
{
    public class ItemVisualObj: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected GameObject descriptionBox;
        [SerializeField] protected TextMeshProUGUI descriptionText;
        
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected ItemSO item;

        public void SetItem(ItemSO itemSo)
        {
            item = Instantiate(itemSo);
            nameText.text = item.itemName;
            descriptionText.text = item.Description;
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