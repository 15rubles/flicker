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

        public int price;
        
        public string itemName;
        
        public string description;

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
                        return Constants.CommonItemBasePrice;
                    case Rarity.Rare:
                        return Constants.RareItemBasePrice;
                    case Rarity.UltraRare:
                        return Constants.UltraRareItemBasePrice;
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