using System.Collections.Generic;
using UnityEngine;

namespace ATH
{
    public class SceneManager : ISceneManager
    {
        private SceneId _activeScene;
        private SceneId _previousScene;
        private Dictionary<SceneId, int> _sceneIdMap;

        public SceneId ActiveScene => _activeScene;
        public SceneId PreviousScene => _previousScene;

        private Dictionary<SceneId, AsyncOperation> _inactiveScene = new();

        public SceneManager()
        {
            _previousScene = SceneId.Preload;

            _sceneIdMap = new Dictionary<SceneId, int>
            {
                { SceneId.Preload, 0 },
                { SceneId.Lobby, 1 },
                { SceneId.Gameplay, 2 }
            };
        }

        public void LoadScene(SceneId sceneId)
        {
            if (!_sceneIdMap.ContainsKey(sceneId)) return;
            _previousScene = _activeScene;
            UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneIdMap[sceneId]);
            _activeScene = sceneId;
        }

        public async Awaitable LoadSceneAsync(SceneId sceneId, bool isAdditive, bool isInactive)
        {
            if (!_sceneIdMap.ContainsKey(sceneId)) return;

            if (!isAdditive)
            {
                _previousScene = _activeScene;
            }

            var operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneIdMap[sceneId],
                isAdditive ? UnityEngine.SceneManagement.LoadSceneMode.Additive : UnityEngine.SceneManagement.LoadSceneMode.Single);

            operation.allowSceneActivation = !isInactive;

            if (isInactive)
            {
                _inactiveScene.Add(sceneId, operation);
            }

            await operation;

            if (!isAdditive)
            {
                _activeScene = sceneId;
            }

        }

        public bool HasInactiveScene(SceneId sceneId)
        {
            return _inactiveScene.ContainsKey(sceneId);
        }

        public void ActivateScene(SceneId sceneId)
        {
            if (!_inactiveScene.ContainsKey(sceneId)) return;

            _inactiveScene[sceneId].allowSceneActivation = true;
            _inactiveScene.Remove(sceneId);
        }
    }
}
