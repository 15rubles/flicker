using Controller;
using UnityEngine;

namespace Utils
{
    public class CreatureSlotViewInfo
    {
        public RectTransform Rect { get; }
        public ZoneController Zone { get; }
        public int Index { get; }

        public CreatureSlotViewInfo()
        {
            
        }

        public CreatureSlotViewInfo(RectTransform rect, ZoneController zone, int index)
        {
            Rect = rect;
            Zone = zone;
            Index = index;
        }
    }
}