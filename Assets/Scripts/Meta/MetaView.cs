using System.Collections;
using UnityEngine;

public class MetaView : MonoBehaviour {
    public void PlayButton() {
        StartCoroutine(ChangeSceneToCore());
    }

    private IEnumerator ChangeSceneToCore() {
        AsyncSceneChanger sceneChanger = new("CoreScene");
        sceneChanger.PreloadScene();
        yield return new WaitWhile(() => !sceneChanger.IsNextSceneLoaded);
        sceneChanger.ChangeScene();
    }

    public void EraseDataButton() {
        PlayerPrefs.DeleteAll();
    }
}