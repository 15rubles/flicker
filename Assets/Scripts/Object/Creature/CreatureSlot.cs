using Controller;
using UnityEngine;

namespace Object.Creature
{
    public class CreatureSlot : MonoBehaviour
    {
        [SerializeField] private CreatureObj creatureObj;
        private CreatureController creatureController;

        public CreatureObj CreatureObj
        {
            get => creatureObj;
            set => creatureObj = value;
        }

        public CreatureController CreatureController => creatureController;

        public void Setup(CreatureObj obj)
        {
            creatureObj = obj;
            creatureObj.Slot = this;
        }

        public void Reset()
        {
            creatureObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        
    }
}