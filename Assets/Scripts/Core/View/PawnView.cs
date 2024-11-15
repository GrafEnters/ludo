using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnView : MonoBehaviour {
    [SerializeField]
    private Color _blue, _red, _green, _yellow;

    [SerializeField]
    private Image _coloredCenter;

    [SerializeField]
    private Button _selectButton;

    public int Cell;
    public int OwnerIndex;
    public int PawnIndex;

    public Action<int, int> OnPawnSelected;

    public void InitColor(TeamColor teamColor) {
        Dictionary<TeamColor, Color> colorsD = new() {
            { TeamColor.Blue, _blue },
            { TeamColor.Red, _red },
            { TeamColor.Green, _green },
            { TeamColor.Yellow, _yellow }
        };
        _coloredCenter.color = colorsD[teamColor];
    }

    public IEnumerator MoveToPos(Vector3 pos) {
        Vector3 starPos = transform.position;
        float time = 0;
        float maxTime = 0.3f;
        while (time < maxTime) {
            time += Time.deltaTime;
            float percent = Mathf.Sin(MathF.PI * time / maxTime);
            Vector3 targetPos = pos + Vector3.up * 30 * percent;
            transform.position = Vector3.Lerp(starPos, targetPos, time / maxTime);
            yield return new WaitForEndOfFrame();
        }

        transform.position = pos;
    }

    public void OnClicked() {
        Debug.Log($"clicked on pawn: [{OwnerIndex},{PawnIndex}] ");
    }

    public void OnSelect() {
        Debug.Log($"selected pawn: [{OwnerIndex},{PawnIndex}] ");
        OnPawnSelected?.Invoke(OwnerIndex, PawnIndex);
    }

    public void ChangeSelectable(bool isSelectable) {
        _selectButton.interactable = isSelectable;
        _selectButton.gameObject.SetActive(isSelectable);
    }
}

public enum TeamColor {
    Green = 0,
    Blue,
    Red,
    Yellow
}