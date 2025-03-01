using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ATH
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration;

        private bool _isActive;

        public bool IsActive => _isActive;

        public async Awaitable Show(string id)
        {
            if (_isActive) return;

            _graphicRaycaster.enabled = true;
            _isActive = true;
            DOTween.To(() => _canvasGroup.alpha, (val) => _canvasGroup.alpha = val, 1, _fadeDuration);
            await Awaitable.WaitForSecondsAsync(_fadeDuration);
        }

        public async Awaitable Hide()
        {
            if (!_isActive) return;

            _isActive = false;
            _graphicRaycaster.enabled = false;
            _canvasGroup.alpha = 1;
            DOTween.To(() => _canvasGroup.alpha, (val) => _canvasGroup.alpha = val, 0, _fadeDuration);
            await Awaitable.WaitForSecondsAsync(_fadeDuration);
        }
    }
}