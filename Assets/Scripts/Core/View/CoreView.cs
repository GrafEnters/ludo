using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoreView : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _diceValue;

    public Action<int> OnPlayerThrowDice, OnPlayerSelectPawnToMove;

    [SerializeField]
    private PawnsViewController _pawnsViewController;

    public PawnsViewController PawnsViewController => _pawnsViewController;

    private void Start() {
        CoreController coreController = new CoreController();
        coreController.Subscribe(this);
        coreController.StartCore();
    }

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
        int res = Random.Range(0, 7);
        _diceValue.text = res.ToString();
        OnPlayerThrowDice?.Invoke(res);
    }

    public void SelectPawnToMove(int pawnIndex) {
        OnPlayerSelectPawnToMove?.Invoke(pawnIndex);
    }

    public void EnablePawnsInteractive() { }
}