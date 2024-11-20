using UnityEngine;

namespace Gilzoide.TutorialHighlight.Internal
{
    public static class Extensions
    {
        /// <summary>
        /// Transforms rectangle from local space to world space 
        /// </summary>
        public static Rect TransformRect(this Transform transform, Rect rect)
        {
            rect.min = transform.TransformPoint(rect.min);
            rect.max = transform.TransformPoint(rect.max);
            return rect;
        }

        /// <summary>
        /// Transforms rectangle from world space to local space.
        /// </summary>
        public static Rect InverseTransformRect(this Transform transform, Rect rect)
        {
            rect.min = transform.InverseTransformPoint(rect.min);
            rect.max = transform.InverseTransformPoint(rect.max);
            return rect;
        }
    }
}
