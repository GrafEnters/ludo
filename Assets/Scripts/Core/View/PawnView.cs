using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PawnView : MonoBehaviour {
    [SerializeField]
    private Color _blue, _red, _green, _yellow;

    [SerializeField]
    private Image _coloredCenter;

    private void Start() {
        InitData((TeamColor)Random.Range(0,4));
    }

    private void InitData(TeamColor teamColor) {
        Dictionary<TeamColor, Color> colorsD = new() {
            { TeamColor.Blue, _blue },
            { TeamColor.Red, _red },
            { TeamColor.Green, _green },
            { TeamColor.Yellow, _yellow }
        };
        _coloredCenter.color = colorsD[teamColor];
    }
}

public enum TeamColor {
    Blue = 0,
    Red,
    Green,
    Yellow
}