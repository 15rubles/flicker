using System.Collections.Generic;
using Entity.Card.Ability;
using Entity.Monster.Ability;
using Object.Monster;
using UnityEngine;

namespace Utils
{
    public static class UtilitiesFunctions
    {
        public static void MoveBefore(GameObject toMove, GameObject target)
        {
            int targetIndex = target.transform.GetSiblingIndex();
            toMove.transform.SetSiblingIndex(targetIndex);
        }

        public static void MoveAfter(GameObject toMove, GameObject target)
        {
            int targetIndex = target.transform.GetSiblingIndex();
            toMove.transform.SetSiblingIndex(targetIndex + 1);
        }

        public static void SetSameParent(GameObject toChange, GameObject target)
        {
            if (toChange.transform.parent != target.transform.parent)
            {
                toChange.transform.SetParent(target.transform.parent);
            }
        }
        
        public static Vector2 ConvertAnchoredPosition(RectTransform from, RectTransform to)
        {
            Vector3 worldPos = from.TransformPoint(from.anchoredPosition);
            Vector3 localPos = to.InverseTransformPoint(worldPos);

            return localPos;
        }
        
        public static bool IsRectTransformFullyOnScreen(RectTransform target)
        {
            Vector3[] corners = new Vector3[4];
            target.GetWorldCorners(corners);

            for (int i = 0; i < 4; i++)
            {
                Vector3 screenPoint = corners[i]; // in world space

                // Since root canvas is Overlay, this behaves like screen space
                if (screenPoint.x < 0 || screenPoint.x > Screen.width ||
                    screenPoint.y < 0 || screenPoint.y > Screen.height)
                {
                    return false;
                }
            }

            return true;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
        
        public static void UseAbilitiesOfType(this List<MonsterAbility> list, AbilityType abilityType, MonsterObject monsterObject)
        {
            foreach (var ability in list.FindAll(a => a.AbilityType == abilityType))
            {
                ability.UseAbility(monsterObject);
            }
        }

    }
}