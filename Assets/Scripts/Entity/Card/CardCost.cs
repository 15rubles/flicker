using System;
using UnityEngine;

namespace Entity.Card
{
    [Serializable]
    public class CardCost
    {
        [SerializeField]
        private int cost;

        public CardCost(int cost)
        {
            this.cost = cost;
        }

        public int Cost
        {
            get => cost;
            set => cost = value;
        }
    }
}