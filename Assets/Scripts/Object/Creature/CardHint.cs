﻿using Object.Card;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Object.Creature
{
    public class CardHint : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI power;
        [SerializeField] private TextMeshProUGUI cardName;
        [SerializeField] private TextMeshProUGUI description;
        
        [SerializeField] private GameObject keywordExplainPrefab;
        [SerializeField] private GameObject keywordExplainPool;
        [SerializeField] private RectTransform boundaries;

        public RectTransform Boundaries => boundaries;
        
        private Entity.Card.Card card;
        
        public Entity.Card.Card Card
        {
            get => card;
            set
            {
                card = value;
                foreach (Transform child in keywordExplainPool.transform) {
                    Destroy(child.gameObject);
                }
                foreach (var keyword in card.keywords)
                {
                    var newKeyword = Instantiate(keywordExplainPrefab.gameObject, keywordExplainPool.transform);
                    newKeyword.GetComponent<KeywordExplain>().UpdateText(keyword);
                }
                UpdateText();
            }
        }
        
        private void UpdateText()
        {
            cardName.text = card.cardName.ToString();
            power.text = card.power.ToString();
            description.text = card.Description;
        }
    }
}