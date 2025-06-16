using Entity.Card.Ability;
using TMPro;
using UnityEngine;

namespace Object.Card
{
    public class KeywordExplain : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keywordName;
        [SerializeField] private TextMeshProUGUI keywordDescription;

        public void UpdateText(KeywordType keywordType)
        {
            keywordName.text = keywordType.ToString();
            keywordDescription.text = keywordType.GetDescription();
        }
    }
}