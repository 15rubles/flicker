using System.Collections.Generic;
using Object;
using UnityEngine;

namespace Controller
{
    public class ZoneController : MonoBehaviour
    {
        public RectTransform rectTransform;
        public CreatureController creatureController;

        protected List<CreatureObj> creatures = new List<CreatureObj>();

        public void AddCreature(CreatureObj creatureObj)
        {
            creatureObj.transform.SetParent(transform);
            creatures.Add(creatureObj);
        }
    }
}