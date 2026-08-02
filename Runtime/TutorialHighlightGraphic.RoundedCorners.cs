#if HAVE_GILZOIDE_ROUNDED_CORNERS
using Gilzoide.RoundedCorners;
using UnityEngine;
using UnityEngine.UI;

namespace Gilzoide.TutorialHighlight
{
    public partial class TutorialHighlightGraphic : IVertexColorProvider
    {
        [Header("Rounded corners")]
        [Tooltip("Inner rounded corner configuration. If Radius is 0, corners will not be rounded.")]
        [SerializeField] RoundedCorner _innerRoundedCorner = new RoundedCorner { Radius = 8, TriangleCount = 8 };

        /// <summary>Inner rounded corner configuration. If Radius is 0, corners will not be rounded.</summary>
        public RoundedCorner InnerRoundedCorner
        {
            get => _innerRoundedCorner;
            set
            {
                _innerRoundedCorner = value;
                SetVerticesDirty();
            }
        }

        protected void OnPopulateMeshInnerRoundedCorner(VertexHelper vh)
        {
            if (_innerRoundedCorner.Radius > 0)
            {
                Rect cutoutRect = CutoutMargin.Add(CutoutRect);
                vh.AddOutsideRoundedRect(cutoutRect, _innerRoundedCorner, this);
            }
        }

        #region IVertexColorProvider

        Color IVertexColorProvider.GetVertexColor(Vector2 normalizedRectPosition)
        {
            return color;
        }

        #endregion
    }
}
#endif