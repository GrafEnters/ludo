using System.Collections;
using TMPro;
using UnityEngine;

public class CoreView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _diceValue;

    public void EndGame() {
        TrySaveFtueComplete();
        StartCoroutine(ChangeSceneToMeta());
    }

    private void TrySaveFtueComplete() {
        if (PlayerPrefs.GetInt("FtueGameComplete", 0) == 1) {
            return;
        }

        PlayerPrefs.SetInt("FtueGameComplete", 1);
        Debug.Log("Ftue Complete");
    }

    private IEnumerator ChangeSceneToMeta() {
        AsyncSceneChanger sceneChanger = new("MetaScene");
        sceneChanger.PreloadScene();
        yield return new WaitWhile(() => !sceneChanger.IsNextSceneLoaded);
        sceneChanger.ChangeScene();
    }

    public void ThrowDice() {
        int res = Random.Range(0, 6);
        _diceValue.text = res.ToString();
    }
}