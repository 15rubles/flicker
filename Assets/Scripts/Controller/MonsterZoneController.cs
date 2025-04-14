using System.Collections.Generic;
using Object;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller
{
    public class MonsterZoneController: MonoBehaviour
    {
        [SerializeField] private RectTransform startPoint;
        [SerializeField] private RectTransform endPoint;
        [SerializeField] private List<RectTransform> monsters;
        [SerializeField] private float maxDistanceBetweenMonsters;
        [SerializeField] private float cardWidth;

        public void AddNewMonster(MonsterObject newMonster)
        {
            var rectTransform = newMonster.gameObject.GetComponent<RectTransform>();
            monsters.Add(rectTransform);
            UpdateMonstersPosition();
        }
        
        public void UpdateMonstersPosition()
        {
            float monstersLen = (monsters.Count - 1) * (cardWidth + maxDistanceBetweenMonsters);
            float monsterZoneLen = (endPoint.anchoredPosition - startPoint.anchoredPosition).x;
            if (monstersLen > monsterZoneLen)
            {
                monstersLen = monsterZoneLen;
            }
            float step = monstersLen / monsters.Count;
            float currentSpawnPoint = (monsterZoneLen - monstersLen) / 2 + startPoint.anchoredPosition.x + step / 2;
            
            foreach (var monster in monsters)
            {
                monster.anchoredPosition = new Vector3(currentSpawnPoint,  startPoint.anchoredPosition.y);
                currentSpawnPoint += step;
            }
        }
    }
}