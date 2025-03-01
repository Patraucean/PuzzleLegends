namespace ATH
{
    public interface ISceneManager
    {
        SceneId ActiveScene { get; }
        SceneId PreviousScene { get; }

        void LoadScene(SceneId sceneId);
    }
}
