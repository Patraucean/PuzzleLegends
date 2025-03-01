using UnityEngine;
using Zenject;

namespace ATH
{
    public class PreloadController : MonoBehaviour
    {
        [SerializeField] private float _delayLoadNextScene = 0.2f;

        [Inject] private ISceneManager _sceneManager;
        [Inject] private ILoadingScreen _loadingScreen;

        private void Start()
        {
            LoadNextScene();
        }

        private async void LoadNextScene()
        {
            await Awaitable.WaitForSecondsAsync(_delayLoadNextScene);

            //Load first level instead of lobby
            //var isOverFirstReturnToLobby = _levelManager.LevelIndex + 1 > _projectDefaults.Get<OnboardingConfiguration>(DefaultsHelperConstants.Onboarding).FirstLevelToLobby;
            //var targetScene = isOverFirstReturnToLobby? SceneId.Lobby : SceneId.Gameplay;

            //if (targetScene == SceneId.Gameplay)
            //{
            //    var setupNextLevel = new SetupNextLevel(_levelPlayContextAggregator, null, _levelDataProvider, _levelManager, _itemProvider);
            //    await setupNextLevel.Execute(null);
            //}

            await _loadingScreen.Show(string.Empty);
            _sceneManager.LoadScene(SceneId.Lobby);
        }
    }
}
