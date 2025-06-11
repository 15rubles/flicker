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
    }
}