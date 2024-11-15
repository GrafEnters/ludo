using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldViewController : MonoBehaviour {
    [SerializeField]
    private CellsList _cells;

    [SerializeField]
    private CellsList[] _startingCells, _startPaths, _finishPaths, _finishedCells;

    private List<Transform[]> _calculatedPaths;

    public int GetPathLength => _calculatedPaths[0].Length+1;
    
    public void PrecalculatePaths() {
        _calculatedPaths = new List<Transform[]>();
        for (int i = 0; i < _startingCells.Length; i++) {
            _calculatedPaths.Add(GetPathForPlayer(i));
        }
    }

    public Vector3 GetPosForCell(int playerIndex, int cell) {
        return _calculatedPaths[playerIndex][cell - 1].position;
    }

    public Vector3 GetPosForStartingCell(int player, int pawnIndex) {
        return _startingCells[player].Cells[pawnIndex].position;
    }

    private Transform[] GetPathForPlayer(int index) {
        var res = _startPaths[index].Cells.ToList();
        res.Add(_finishPaths[ClampedIndex(index + 1)].Cells[0]);
        res.AddRange(_startPaths[ClampedIndex(index + 1)].Cells);
        res.Add(_finishPaths[ClampedIndex(index + 2)].Cells[0]);
        res.AddRange(_startPaths[ClampedIndex(index + 2)].Cells);
        res.Add(_finishPaths[ClampedIndex(index + 3)].Cells[0]);
        res.AddRange(_startPaths[ClampedIndex(index + 3)].Cells);
        res.AddRange(_finishPaths[ClampedIndex(index)].Cells);
        return res.ToArray();
    }

    private int ClampedIndex(int before) {
        return before % _startingCells.Length;
    }
}