using System;
using System.Collections.Generic;
using Entity.Item;
using UnityEngine;

namespace Entity
{
    [Serializable]
    public class RunState
    {
        [SerializeField] private Deck deck;
        [SerializeField] private DeckSo deckSo;
        [SerializeField] private int money;
        [SerializeField] private int battleCounter = 1;
        [SerializeField] private List<ItemSO> items = new List<ItemSO>();
        [SerializeField] private int hp;
        [SerializeField] private int maxHp;
        [SerializeField] private int maxMulliganCount = 1;

        public int MaxMulliganCount
        {
            get => maxMulliganCount;
            set => maxMulliganCount = value;
        }

        public int Hp
        {
            get => hp;
            set => hp = value > maxHp ? maxHp : value;
        }

        public List<ItemSO> Items
        {
            get => items;
            set => items = value;
        }
        
        public Deck Deck
        {
            get => deck;
            set => deck = value;
        }

        public DeckSo DeckSo
        {
            get => deckSo;
            set => deckSo = value;
        }

        public int Money
        {
            get => money;
            set => money = value;
        }

        public int BattleCounter
        {
            get => battleCounter;
            set => battleCounter = value;
        }

        public void AddItem(ItemSO item)
        {
            items.Add(item);
            item.ability.EnableItem();
        }
    }
}