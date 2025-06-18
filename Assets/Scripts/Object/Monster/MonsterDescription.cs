using TMPro;
using UnityEngine;

namespace Object.Monster
{
    public class MonsterDescription : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;

        public string Description
        {
            set => description.text = value;
        }
        
    }
}