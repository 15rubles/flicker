using Controller;
using UnityEngine;

namespace Object.Monster
{
    public class MonsterSlot : MonoBehaviour
    {
        [SerializeField] private MonsterObject monsterObj;
        private MonsterController monsterController;

        public MonsterObject MonsterObj
        {
            get => monsterObj;
            set => monsterObj = value;
        }

        public void SetMonsterController(MonsterController controller)
        {
            monsterController = controller;
        }

        public void Setup(MonsterObject obj)
        {
            monsterObj = obj;
            monsterObj.Slot = this;
        }

        public void Reset()
        {
            monsterObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        
    }
}