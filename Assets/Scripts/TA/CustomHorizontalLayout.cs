using UnityEngine;
using UnityEngine.UI;

namespace ATH
{
    [RequireComponent(typeof(RectTransform))]
    public class CustomHorizontalLayout : MonoBehaviour
    {
        [SerializeField] private bool _setOnAwake;

        private async void Awake()
        {
            await Awaitable.NextFrameAsync();
            await Awaitable.NextFrameAsync();
            if (_setOnAwake)
            {
                Distribute();
            }
        }

        [ContextMenu("Distribute")]
        private void Distribute()
        {
            var totalWidth = 0f;
            var childCount = transform.childCount;

            var scaler = GetComponentInParent<CanvasScaler>();
            var scaleFactor = scaler.GetComponent<Canvas>().scaleFactor;

            var first = transform.GetChild(0);
            if (first == null) return;

            var firstRt = first.GetComponent<RectTransform>();
            var firstSize = RectTransformUtility.CalculateRelativeRectTransformBounds(firstRt).size.x;
            firstRt.transform.position = transform.position + Vector3.right * firstSize / 2f * scaleFactor;
            totalWidth += firstSize;

            for (int i = 0; i < childCount - 1; i++)
            {
                var currentChildRt = transform.GetChild(i).GetComponent<RectTransform>();
                var nextChildRt = transform.GetChild(i + 1).GetComponent<RectTransform>();
                var currentChildSize = RectTransformUtility.CalculateRelativeRectTransformBounds(currentChildRt).size;
                var nextChildSize = RectTransformUtility.CalculateRelativeRectTransformBounds(nextChildRt).size;

                var positionOffset = currentChildSize.x / 2f + nextChildSize.x / 2f;
                positionOffset *= scaleFactor;

                nextChildRt.transform.position = new Vector2(currentChildRt.position.x + positionOffset, currentChildRt.position.y);
                totalWidth += nextChildSize.x;
            }

            for (int i = 0; i < childCount; i++)
            {
                var currentChildRt = transform.GetChild(i).GetComponent<RectTransform>();
                currentChildRt.transform.position += Vector3.left * totalWidth / 2f * scaleFactor;
            }

            LayoutRebuilder.MarkLayoutForRebuild(transform as RectTransform);
        }
    }
}
