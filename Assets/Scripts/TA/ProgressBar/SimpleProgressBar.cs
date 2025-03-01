using System.Threading.Tasks;
using UnityEngine;
using System;

namespace ATH
{
    public class SimpleProgressBar : MonoBehaviour
    {
        [SerializeField] private float _width = 100f;
        [SerializeField] private RectTransform _fill;

        private float _progress;

        public float Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                ProgressUpdated?.Invoke();
            }
        }

        public Action ProgressUpdated;

        /// <summary>
        /// If speed = 0, charge instantly
        /// </summary>
        public async Task SetProgress(float progress, float speed = 0)
        {
            await SetProgress(_fill.sizeDelta.x / _width, progress, speed);
        }

        public async Task SetProgress(float startProgress, float progress, float speed = 0)
        {
            progress = Mathf.Clamp01(progress);

            if (speed == 0)
            {
                Progress = progress;
                _fill.sizeDelta = new Vector2(_width * progress, _fill.sizeDelta.y);
                return;
            }
            try
            {
                await ChargeBar(startProgress, _width * progress, speed);
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        private async Task ChargeBar(float startProgress, float targetWidth, float speed)
        {
            try
            {
                var sizeVector = new Vector2(_fill.sizeDelta.x, _fill.sizeDelta.y);
                var targetProgress = targetWidth / _width;
                var currentProgress = startProgress;
                var progressDiff = Mathf.Abs(targetProgress - currentProgress);

                for (float i = 0; i < progressDiff; i += Time.deltaTime * speed)
                {
                    sizeVector.x = (currentProgress + i) * _width;
                    Progress = currentProgress + i;
                    _fill.sizeDelta = sizeVector;
                    await Awaitable.EndOfFrameAsync(destroyCancellationToken);
                }

                _fill.sizeDelta = new Vector2(targetWidth, _fill.sizeDelta.y);
            }
            catch (Exception e)
            {

                Debug.LogWarning(e);
            }
        }
    }
}
