using UnityEngine;

namespace ATH
{
    public class NavigationBar : MonoBehaviour
    {
        [SerializeField] private TabBar _tabBar;
        [SerializeField] private ScreenNavigation _screenNavigation;

        private void Awake()
        {
            _tabBar.TabChanged.AddListener(OnTabChanged);
        }

        private void OnTabChanged(int index)
        {
            _screenNavigation.Navigate(index);
        }
    }
}
