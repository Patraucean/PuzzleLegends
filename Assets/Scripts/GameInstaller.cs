using ATH;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    [SerializeField] private LoadingScreen _loadingScreen;

    public override void InstallBindings()
    {
        var sceneManager = new SceneManager();

        Container.Bind<ISceneManager>().FromInstance(sceneManager).AsSingle();
        Container.Bind<ILoadingScreen>().FromInstance(_loadingScreen).AsSingle();
    }
}