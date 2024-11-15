using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnsViewController: MonoBehaviour {

    [SerializeField]
    private Transform _pawnsHolder;

    [SerializeField]
    private PawnView _pawnPrefab;

    [SerializeField]
    private CellsList _cells;
    [SerializeField]
    private CellsList[] _startingCells;

    private List<PawnView>[] _playersPawns;
    
    public void CreatePawns(int playersAmount, int pawnsAmount, Action<int,int> onPawnClicked) {
        _playersPawns = new List<PawnView>[playersAmount];
        for (int i = 0; i < playersAmount; i++) {
            _playersPawns[i] = new List<PawnView>();
            for (int j = 0; j < pawnsAmount; j++) {
                PawnView p = Instantiate(_pawnPrefab, _pawnsHolder);
                p.OwnerIndex = i;
                p.PawnIndex = j;
                p.transform.position = GetPosForStartingCell(i, j);
                p.InitColor((TeamColor)i);
                p.OnPawnSelected = onPawnClicked;
                p.ChangeSelectable(i != 0);
                _playersPawns[i].Add(p);
            }
        }
    }

    public void MovePawn(int playerOwner, int pawnIndex, int steps) {
        StartCoroutine(MovePawnCoroutine(playerOwner, pawnIndex, steps));
    }
    
    private IEnumerator MovePawnCoroutine(int playerOwner, int pawnIndex, int steps) {
        for (int i = 0; i < steps; i++) {
            yield return StartCoroutine(MovePawnOneStep(playerOwner, pawnIndex));
        }
    } 

    private IEnumerator MovePawnOneStep(int playerOwner, int pawnIndex) {
        PawnView p = _playersPawns[playerOwner][pawnIndex];
        p.Cell++;
        Vector3 targetPos =  GetPosForCell(p.Cell);
        yield return StartCoroutine(p.MoveToPos(targetPos));
    }

    private Vector3 GetPosForCell(int cell) {
        return _cells.Cells[cell-1].position;
    }
    
    private Vector3 GetPosForStartingCell(int player, int pawnIndex) {
        return _startingCells[player].Cells[pawnIndex].position;
    }

    public void SetPawnsInteractive(List<int> interactivePawns) {
        for (int i = 0; i < _playersPawns[0].Count; i++) {
            _playersPawns[0][i].ChangeSelectable(interactivePawns.Contains(i));
        }
    }

    public void ClearPawnsInteractive() {
        for (int i = 0; i < _playersPawns[0].Count; i++) {
            _playersPawns[0][i].ChangeSelectable(false);
        }
    }
}
