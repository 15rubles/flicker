using System;
using System.Collections.Generic;
using Entity.Item;
using UnityEngine;

namespace Entity
{
    [Serializable]
    public class Reward
    {
        [SerializeField] private int money;
        [SerializeField] private List<ItemSO> items;
        [SerializeField] private List<Card.Card> cards;

        public int Money => money;

        public List<ItemSO> Items => items;

        public List<Card.Card> Cards => cards;
    }
}