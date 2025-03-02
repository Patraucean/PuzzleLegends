using BlockPuzzleGameToolkit.Scripts.LevelsData;
using BlockPuzzleGameToolkit.Scripts.System;
using System;
using UnityEngine;
using Zenject;

namespace ATH
{
    public class GameplayController : MonoBehaviour
    {
        [Inject] private ILoadingScreen _loadingScreen;

        private async void Start()
        {
            try
            {
                await Awaitable.EndOfFrameAsync();
                await _loadingScreen.Hide();

                GameDataManager.SetLevel(Resources.Load<Level>("Levels/Level_" + GameDataManager.GetLevelNum()));
                GameManager.instance.OpenGame();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
