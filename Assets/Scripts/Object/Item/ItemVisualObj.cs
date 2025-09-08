using Entity.Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Object.Item
{
    public class ItemVisualObj: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected GameObject descriptionBox;
        [SerializeField] protected TextMeshProUGUI descriptionText;
        
        [SerializeField] protected TextMeshProUGUI nameText;
        [SerializeField] protected ItemSO item;
        [SerializeField] private Image icon;
        [SerializeField] private int targetHeight;

        public void SetItem(ItemSO itemSo)
        {
            item = Instantiate(itemSo);
            nameText.text = item.itemName;
            descriptionText.text = item.Description;
            icon.sprite = item.sprite;
            UtilitiesFunctions.SetNativeThenScaleToHeight(icon, targetHeight);
            
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