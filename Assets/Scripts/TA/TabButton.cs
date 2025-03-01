using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ATH
{
    public class TabButton : Button
    {
        [SerializeField] private UnityEvent _activated;
        [SerializeField] private UnityEvent _deactivated;

        public event Action Selected;

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(() => Selected?.Invoke());
        }

        public void SetSelected(bool isSelected)
        {
            var targetEvent = isSelected ? _activated : _deactivated;
            targetEvent.Invoke();
        }
    }
}
