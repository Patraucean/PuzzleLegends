using System;
using UnityEngine;
using Zenject;

namespace ATH
{
    public class LobbyController : MonoBehaviour
    {
        [Inject] private ISceneManager _sceneManager;
        [Inject] private ILoadingScreen _loadingScreen;

        private async void Start()
        {
            try
            {
                await Awaitable.EndOfFrameAsync();
                await _loadingScreen.Hide();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
