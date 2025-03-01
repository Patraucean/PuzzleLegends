using System.Collections.Generic;

namespace ATH
{
    public class SceneManager : ISceneManager
    {
        private SceneId _activeScene;
        private SceneId _previousScene;
        private Dictionary<SceneId, int> _sceneIdMap;

        public SceneId ActiveScene => _activeScene;
        public SceneId PreviousScene => _previousScene;

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
    }
}
