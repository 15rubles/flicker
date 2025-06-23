using System;
using System.Collections.Generic;
using System.Linq;
using Entity.Item.Ability;
using Sirenix.OdinInspector;
using UnityEngine;

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