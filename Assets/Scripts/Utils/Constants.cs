using UnityEngine;

namespace Utils
{
    public static class Constants
    {
        public const int COMMON_ITEM_BASE_PRICE = 6;
        public const int RARE_ITEM_BASE_PRICE = 10;
        public const int ULTRA_RARE_ITEM_BASE_PRICE = 15;
        public const string TRIGGER_TO_DESCRIPTION_CONNECTOR = " => \n";
        public const float SELL_PERCENTAGE = 0.6f;
        public readonly static Color32 ExtraPowerColor = new Color32(92, 232, 79, 255);
        public readonly static Color32 DamagedPowerColor = new Color32(232, 79, 79, 255);
        public readonly static Color32 BasePowerColor = Color.black;
    }
}