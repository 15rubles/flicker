using System;
using UnityEngine;

namespace Entity.Card
{
    [Serializable]
    public class CardStats
    {
        [SerializeField]
        private int power;
        [SerializeField]
        private int toughness;
//  convert to one stat
        public CardStats(int power, int toughness)
        {
            this.power = power;
            this.toughness = toughness;
        }

        public int Power
        {
            get => power;
            set => power = value;
        }

        public int Toughness
        {
            get => toughness;
            set => toughness = value;
        }
    }
}