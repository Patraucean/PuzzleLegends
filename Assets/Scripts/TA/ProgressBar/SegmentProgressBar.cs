using UnityEngine;
using UnityEngine.UI;

namespace ATH
{
    public class SegmentProgressBar : MonoBehaviour
    {
        [SerializeField] private HorizontalOrVerticalLayoutGroup _segmentHolder;

        [Header("Sprites")]
        [SerializeField] private Sprite _startSprite;
        [SerializeField] private Sprite _centerSprite;
        [SerializeField] private Sprite _endSprite;

        [Header("Colors")]
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        private int _maxSegments;
        private Image[] _segments;

        public void Init(int maxSegments)
        {
            _maxSegments = maxSegments;
            _segments = new Image[_maxSegments];

            // Clear existing children
            foreach (Transform child in _segmentHolder.transform)
            {
                Destroy(child.gameObject);
            }

            // Generate segments
            for (int i = 0; i < _maxSegments; i++)
            {
                GameObject segment = new();
                segment.transform.SetParent(_segmentHolder.transform);
                _segments[i] = segment.AddComponent<Image>();

                // Assign default inactive sprite
                _segments[i].sprite = i == 0 ? _startSprite :
                                     i == _maxSegments - 1 ? _endSprite : _centerSprite;
            }
        }

        public void SetProgress(int currentProgress)
        {
            for (int i = 0; i < _maxSegments; i++)
            {
                _segments[i].color = i < currentProgress ? _activeColor : _inactiveColor;
            }
        }
    }
}