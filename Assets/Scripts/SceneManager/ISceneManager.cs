using UnityEngine;

namespace ATH
{
    public interface ISceneManager
    {
        SceneId ActiveScene { get; }
        SceneId PreviousScene { get; }

        bool HasInactiveScene(SceneId sceneId);
        void ActivateScene(SceneId sceneId);
        void LoadScene(SceneId sceneId);
        Awaitable LoadSceneAsync(SceneId sceneId, bool isAdditive, bool isInactive);
    }
}
