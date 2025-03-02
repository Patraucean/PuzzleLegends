using ATH;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class LobbyPlayButton : MonoBehaviour
{
    [Inject] private ISceneManager _sceneManager;
    [Inject] private ILoadingScreen _loadingScreen;

    private Button _mainButton;

    private void Awake()
    {
        _mainButton = GetComponent<Button>();
        _mainButton.onClick.AddListener(OnPlayPressed);
    }

    private async void OnPlayPressed()
    {
        await _loadingScreen.Show(string.Empty);
        _sceneManager.LoadScene(SceneId.Gameplay);
    }
}
