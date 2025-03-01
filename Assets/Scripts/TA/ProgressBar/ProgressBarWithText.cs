using System.Threading.Tasks;
using UnityEngine;
using System;
using TMPro;

namespace ATH
{
    public class ProgressBarWithText : SimpleProgressBar
    {
        [SerializeField] private TextMeshProUGUI _amount;

        public async Task SetProgress(int start, int end, int max, float speed)
        {
            if (max == 0)
            {
                Debug.LogError($"Can't set max progress to a progress bar to be equal to 0. {gameObject}");
                return;
            }

            if (speed == 0)
            {
                _amount.text = $"{end}/{max}";
            }
            else
            {
                try
                {
                    UpdateAmount(start, end, max, speed);
                }
                catch (OperationCanceledException) { }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            await SetProgress(start / (float)max, end / (float)max, speed);
        }

        private async void UpdateAmount(int start, int end, int max, float speed)
        {
            var progressDiff = Mathf.Abs((end - start) / (float)max);
            var directionMultiplier = end - start > 0 ? 1 : -1;

            for (float i = 0; i < progressDiff; i += Time.deltaTime * speed)
            {
                _amount.text = $"{(int)(start + max * i * directionMultiplier)}/{max}";
                await Awaitable.EndOfFrameAsync(destroyCancellationToken);
            }

            _amount.text = $"{end}/{max}";
        }
    }
}
