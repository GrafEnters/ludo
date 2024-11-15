using System.Collections;
using UnityEngine;

public class Loader : MonoBehaviour {
    private string _nextSceneName;
    private bool _isNextSceneLoaded;

    void Start() {
        StartCoroutine(LoadingLogic());
    }

    private IEnumerator LoadingLogic() {
        Debug.Log("[Loading] Started");
        _nextSceneName = DecideNextScene();

        AsyncSceneChanger sceneChanger = new(_nextSceneName);
        sceneChanger.PreloadScene();
        yield return new WaitWhile(() => !sceneChanger.IsNextSceneLoaded);
        Debug.Log("[Loading] Next scene loaded");
        sceneChanger.ChangeScene();
    }

    private string DecideNextScene() {
        return PlayerPrefs.GetInt("FtueGameComplete", 0) == 1 ? "MetaScene" : "CoreScene";
    }
}