using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ATH
{
    public class TabBar : MonoBehaviour
    {
        [SerializeField] private TabButton _defaultTab;
        [SerializeField] private List<TabButton> _tabButtons = new List<TabButton>();

        private TabButton _activeTab;

        private int _activeTabIndex;

        public UnityEvent<int> TabChanged;

        private void Awake()
        {
            if (_defaultTab != null)
            {
                _activeTab = _defaultTab;
                _activeTabIndex = _tabButtons.IndexOf(_activeTab);
                _activeTab.SetSelected(true);
            }

            for (int i = 0; i < _tabButtons.Count; i++)
            {
                var temp = i;
                _tabButtons[i].Selected += delegate { OnTabSelected(temp); };
            }
        }

        private void OnTabSelected(int index)
        {
            if (_activeTabIndex == index) return;
            if (_activeTab != null) _activeTab.SetSelected(false);

            _activeTabIndex = index;
            _activeTab = _tabButtons[index];
            _activeTab.SetSelected(true);
            TabChanged?.Invoke(index);
        }
    }
}
