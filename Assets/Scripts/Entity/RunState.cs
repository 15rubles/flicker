using System;
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
        
        //TODO add items list;
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
    }
}