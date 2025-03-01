using UnityEngine;

namespace ATH
{
    public interface ILoadingScreen
    {
        bool IsActive { get; }

        Awaitable Show(string id);
        Awaitable Hide();
    }
}