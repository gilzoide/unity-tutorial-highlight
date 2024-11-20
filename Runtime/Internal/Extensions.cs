using UnityEngine;

namespace Gilzoide.TutorialHighlight.Internal
{
    public static class Extensions
    {
        public static Rect ConvertLocalRect(this RectTransform rectTransform, RectTransform targetRectTransform)
        {
            Rect rect = rectTransform.rect;
            rect.min = targetRectTransform.InverseTransformPoint(rectTransform.TransformPoint(rect.min));
            rect.max = targetRectTransform.InverseTransformPoint(rectTransform.TransformPoint(rect.max));
            rect.position += targetRectTransform.pivot * targetRectTransform.rect.size;
            return rect;
        }
    }
}
