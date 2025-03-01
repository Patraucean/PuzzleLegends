using TMPro;
using UnityEngine;

namespace ATH
{
    public class DynamicTextSetter : MonoBehaviour
    {
        [SerializeField] private string _format;
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(int value)
        {
            _text.SetText(string.Format(_format, value));
        }

        public void SetText(int value1, int value2)
        {
            _text.SetText(string.Format(_format, value1, value2));
        }
    }
}
