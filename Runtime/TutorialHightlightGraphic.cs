using Gilzoide.TutorialHighlight.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.TutorialHighlight
{
    [RequireComponent(typeof(CanvasRenderer)), ExecuteAlways]
    public class TutorialHightlightGraphic : MaskableGraphic, ICanvasRaycastFilter
    {
        [Header("Cutout")]
        [SerializeField] private Rect _cutoutRect;
        [SerializeField] private RectTransform _cutoutObject;
        [SerializeField] private RectOffset _cutoutMargin;

        public Rect CutoutRect
        {
            get => _cutoutObject ? _cutoutObject.ConvertLocalRect(rectTransform) : _cutoutRect;
            set
            {
                _cutoutRect = value;
                SetVerticesDirty();
            }
        }
        
        public RectTransform CutoutObject
        {
            get => _cutoutObject;
            set
            {
                _cutoutObject = value;
                SetVerticesDirty();
            }
        }

        public RectOffset CutoutMargin
        {
            get => _cutoutMargin;
            set
            {
                _cutoutMargin = value;
                SetVerticesDirty();
            }
        }

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, sp, eventCamera, out Vector2 localPoint);
            return !LocalCutoutRect.Contains(localPoint);
        }

        public Rect LocalCutoutRect
        {
            get
            {
                Rect cutoutRect = CutoutRect;
                return new Rect(rectTransform.rect.position + cutoutRect.position, cutoutRect.size);
            }
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            Rect rect = rectTransform.rect;
            Rect cutoutRect = _cutoutMargin.Add(CutoutRect);
            Color32 color32 = color;

            // Left rectangle
            vh.AddVert(new Vector3(rect.xMin, rect.yMin), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin, rect.yMax), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMin, rect.yMax), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMin, rect.yMin), color32, new Vector4(0, 0));
            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(2, 3, 0);
            
            // Right rectangle
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMax, rect.yMin), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMax, rect.yMax), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMax, rect.yMax), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMax, rect.yMin), color32, new Vector4(0, 0));
            vh.AddTriangle(4, 5, 6);
            vh.AddTriangle(6, 7, 4);

            // Top rectangle
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMin, rect.yMin + cutoutRect.yMax), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMin, rect.yMax), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMax, rect.yMax), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMax, rect.yMin + cutoutRect.yMax), color32, new Vector4(0, 0));
            vh.AddTriangle(8, 9, 10);
            vh.AddTriangle(10, 11, 8);

            // Bottom rectangle
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMin, rect.yMin), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMin, rect.yMin + cutoutRect.yMin), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMax, rect.yMin + cutoutRect.yMin), color32, new Vector4(0, 0));
            vh.AddVert(new Vector3(rect.xMin + cutoutRect.xMax, rect.yMin), color32, new Vector4(0, 0));
            vh.AddTriangle(12, 13, 14);
            vh.AddTriangle(14, 15, 12);
        }
    }
}
