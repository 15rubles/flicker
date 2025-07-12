using System;
using System.Collections.Generic;
using System.Linq;
using Entity.Card;
using Entity.Item.Ability;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Entity.Item
{
    [CreateAssetMenu(fileName = "Item", menuName = "SOs/Item", order = 1)]
    public class ItemSO : ScriptableObject
    {
        [LabelText("Item Type")]
        public ItemType itemType;

        [TypeFilter(nameof(GetCompatibleAbilityTypes))]
        [SerializeReference]
        // [InlineProperty, HideLabel]
        public ItemAbility ability;

        [Header("If price == 0 will use default value")]
        public int price;
        
        public string itemName;
        
        [TextArea]
        [SerializeField]
        private string description;

        public string Description => ability.ItemType.GetDescription() + Constants.TRIGGER_TO_DESCRIPTION_CONNECTOR + description;

        public Rarity rarity = Rarity.Common;

        public int Price
        {
            get
            {
                if (price != 0)
                    return price;

                switch (rarity)
                {
                    case Rarity.Common:
                        return Constants.COMMON_ITEM_BASE_PRICE;
                    case Rarity.Rare:
                        return Constants.RARE_ITEM_BASE_PRICE;
                    case Rarity.UltraRare:
                        return Constants.ULTRA_RARE_ITEM_BASE_PRICE;
                }
                return price;
            }
        }

        private IEnumerable<Type> GetCompatibleAbilityTypes()
        {
            // Find all non-abstract subclasses of Ability
            var allTypes = typeof(ItemAbility).Assembly
                                              .GetTypes()
                                              .Where(t => t.IsSubclassOf(typeof(ItemAbility)) && !t.IsAbstract);

            return allTypes
                .Where(t =>
                {
                    // Create a dummy instance to check type
                    var temp = Activator.CreateInstance(t) as ItemAbility;
                    return temp != null && temp.ItemType == itemType;
                });
        }
    }
}