using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gilzoide.TutorialHighlight
{
    [ExecuteAlways]
    public class TutorialHightlighController : MonoBehaviour
    {
        [SerializeField] private TutorialHightlightGraphic _tutorialHightlightGraphic;
        [SerializeField] private List<RectTransform> _tutorialObjects = new List<RectTransform>();
        [SerializeField, Min(0)] private int _tutorialStep;

        [Header("Events")]
        public UnityEvent<int> OnTutorialStep;
        public UnityEvent OnTutorialEnded;

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

        public void Refresh(bool invokeEvents = true)
        {
            if (_tutorialHightlightGraphic == null || !isActiveAndEnabled)
            {
                return;
            }

            if (_tutorialStep < _tutorialObjects.Count)
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

        [ContextMenu("Begin Tutorial")]
        public void BeginTutorial()
        {
            _tutorialStep = 0;
            Refresh();
        }
        
        [ContextMenu("End Tutorial")]
        public void EndTutorial()
        {
            _tutorialStep = _tutorialObjects.Count;
            Refresh();
        }

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
