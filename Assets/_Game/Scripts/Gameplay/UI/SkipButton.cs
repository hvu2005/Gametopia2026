
    using System;
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class SkipButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private RectTransform rect;
        private Vector3 originalPos;

        private void Start()
        {
            
            rect = GetComponent<RectTransform>();
            originalPos = rect.localPosition;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (AudioController.Instance != null)
            {
                AudioController.Instance.PlaySfx(AudioController.AudioKeys.UiHover);
            }

            rect.DOLocalMoveY(originalPos.y + 15f, 0.15f);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            rect.DOLocalMoveY(originalPos.y, 0.15f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (AudioController.Instance != null)
            {
                AudioController.Instance.PlaySfx(AudioController.AudioKeys.UiClick);
            }

            EventBus.Emit<bool>(ItemEventType.Skip, true);
        }
    }
