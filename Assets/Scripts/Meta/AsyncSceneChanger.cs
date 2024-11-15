using UnityEngine.SceneManagement;

public class AsyncSceneChanger {
    private string _nextSceneName;
    private bool _isNextSceneLoaded;

    public bool IsNextSceneLoaded => _isNextSceneLoaded;

    public AsyncSceneChanger(string nextSceneName) {
        _nextSceneName = nextSceneName;
    }

    public void PreloadScene() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode loadSceneMode) {
        if (arg0.name == _nextSceneName) {
            _isNextSceneLoaded = true;
        }
    }

    public void ChangeScene() {
        if (!_isNextSceneLoaded) {
            return;
        }

        SceneManager.LoadSceneAsync(_nextSceneName);
    }
}