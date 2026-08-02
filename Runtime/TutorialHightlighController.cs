using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.TutorialHighlight
{
    [ExecuteAlways]
    public class TutorialHightlighController : MonoBehaviour
    {
        [Tooltip("Graphic used to highlight the tutorial objects. If null, will be searched for using GetComponent.")]
        [SerializeField] private TutorialHightlightGraphic _tutorialHightlightGraphic;
        
        [Tooltip("Objects to highlight during the tutorial. The order of the objects in this list defines the order of the tutorial steps.")]
        [SerializeField] private List<RectTransform> _tutorialObjects = new List<RectTransform>();
        
        [Tooltip("Current tutorial step.")]
        [SerializeField, Min(0)] private int _tutorialStep;

        [Header("Events")]
        public UnityEvent<int> OnTutorialStep;
        public UnityEvent OnTutorialEnded;

        /// <summary>
        /// Graphic used to highlight the tutorial objects.
        /// </summary>
        public TutorialHightlightGraphic TutorialHightlightGraphic
        {
            get => _tutorialHightlightGraphic;
            set
            {
                _tutorialHightlightGraphic = value;
                Refresh(false);
            }
        }

        public List<RectTransform> TutorialObjects => _tutorialObjects;

        /// <summary>
        /// Current tutorial step. Set to -1 to end the tutorial.
        /// </summary>
        public int TutorialStep
        {
            get => _tutorialStep;
            set
            {
                _tutorialStep = value;
                Refresh();
            }
        }

        public bool TutorialEnded => _tutorialStep < 0 || _tutorialStep >= _tutorialObjects.Count;

        private void Awake()
        {
            if (_tutorialHightlightGraphic == null)
            {
                _tutorialHightlightGraphic = GetComponent<TutorialHightlightGraphic>();
            }
        }

        private void Start()
        {
            Refresh();
        }

        /// <summary>
        /// Refreshes the tutorial highlight graphic to reflect the current tutorial step.
        /// If the tutorial is ended, the highlight graphic will be disabled and the <see cref="OnTutorialEnded"/> event will be invoked.
        /// Otherwise, the highlight graphic will be enabled and the <see cref="OnTutorialStep"/> event will be invoked with the current step index.
        /// </summary>
        /// <param name="invokeEvents">Whether to invoke <see cref="OnTutorialStep"/> and <see cref="OnTutorialEnded"/> events.</param>
        public void Refresh(bool invokeEvents = true)
        {
            if (_tutorialHightlightGraphic == null || !isActiveAndEnabled)
            {
                return;
            }

            if (!TutorialEnded)
            {
                _tutorialHightlightGraphic.CutoutObject = _tutorialObjects[_tutorialStep];
                _tutorialHightlightGraphic.enabled = true;
                if (invokeEvents)
                {
                    OnTutorialStep.Invoke(_tutorialStep);
                }
            }
            else
            {
                _tutorialHightlightGraphic.CutoutObject = null;
                _tutorialHightlightGraphic.enabled = false;
                if (invokeEvents)
                {
                    OnTutorialEnded.Invoke();
                }
            }
        }

        /// <summary>
        /// Begins or rewinds the tutorial.
        /// </summary>
        [ContextMenu("Begin Tutorial")]
        public void BeginTutorial()
        {
            _tutorialStep = 0;
            Refresh();
        }
        
        /// <summary>
        /// Ends the tutorial.
        /// The highlight graphic will be disabled and the <see cref="OnTutorialEnded"/> event will be invoked.
        /// </summary>
        [ContextMenu("End Tutorial")]
        public void EndTutorial()
        {
            _tutorialStep = _tutorialObjects.Count;
            Refresh();
        }

        /// <summary>
        /// Advances the tutorial to the next step, highlighting the next object in the <see cref="TutorialObjects"/> list.
        /// If the tutorial is already at the last step, it will end the tutorial and <see cref="OnTutorialEnded"/> will be invoked.
        /// Otherwise, the <see cref="OnTutorialStep"/> event will be invoked with the new step index.
        /// </summary>
        [ContextMenu("Advance Tutorial Step")]
        public void AdvanceTutorialStep()
        {
            if (TutorialEnded)
            {
                return;
            }
            _tutorialStep++;
            Refresh();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Refresh();
        }
#endif
    }
}
