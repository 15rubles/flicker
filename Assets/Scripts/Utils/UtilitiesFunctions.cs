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
            // Get world position of 'from'
            Vector3 worldPos = from.TransformPoint(from.anchoredPosition);

            // Convert world position to local position of 'to'
            Vector3 localPos = to.InverseTransformPoint(worldPos);

            return localPos;
        }
    }
}