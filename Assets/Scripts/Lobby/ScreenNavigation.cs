using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ATH
{
    public class ScreenNavigation : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> _screens;
        [SerializeField] private RectTransform _rootToMove;
        [SerializeField] private float _duration;

        private float _scaleFactor;
        private Vector2 _initialPosition;

        private void Awake()
        {
            _scaleFactor = GetComponentInParent<CanvasScaler>().GetComponent<Canvas>().scaleFactor;
            _initialPosition = _rootToMove.anchoredPosition;
        }

        public void Navigate(int index)
        {
            if (index < 0 || index >= _screens.Count)
            {
                Debug.LogError("[SCREEN_NAVIGATION] Index out of range on navigation call");
                return;
            }

            //Handle canceling
            var currentPosition =_rootToMove.anchoredPosition.x;
            var targetPosition = (_initialPosition - _screens[index].anchoredPosition).x;

            DOTween.To(() => currentPosition,
                (val) => { 
                    currentPosition = val; 
                    _rootToMove.anchoredPosition = new Vector3(currentPosition, _rootToMove.anchoredPosition.y); 
                }, 
                targetPosition, 
                _duration);
        }
    }
}
