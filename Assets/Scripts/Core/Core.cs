using System;
using System.Collections.Generic;

public class Core {
    private int _pathLength = 20;
    private CoreModel _model;
    public int PlayersAmount => _model.CorePlayerModels.Length;
    public int PawnsPerPlayer => _model.CorePlayerModels[0].Pawns.Length;

    public void SetUpModel(string[] players, int pathLength) {
        _pathLength = pathLength;
        _model = new CoreModel();
        _model.CorePlayerModels = new CorePlayerModel[players.Length];
        _model.CoreFieldModel = new CoreFieldModel(_pathLength, players.Length);
        for (int i = 0; i < _model.CorePlayerModels.Length; i++) {
            _model.CorePlayerModels[i] = new CorePlayerModel(players[i]);
            _model.CoreFieldModel.Cells[0, i] = _model.CorePlayerModels[i].Pawns.Length;
        }
    }

    public void PlayerMakeMove(int player, int pawn, int steps) {
        int curPawnCell = _model.CorePlayerModels[player].Pawns[pawn];
        _model.CoreFieldModel.Cells[curPawnCell, player]--;
        if (_model.CoreFieldModel.Cells[curPawnCell, player] < 0) {
            throw new Exception("invalid move");
        }

        int cellAfter = curPawnCell + steps;
        _model.CoreFieldModel.Cells[cellAfter, player]++;
        _model.CorePlayerModels[player].Pawns[pawn] = cellAfter;

        TryKillOtherPawns(player, cellAfter);
    }

    private void TryKillOtherPawns(int player, int cellAfter) {
        _model.CoreFieldModel.Cells[cellAfter, player]++;
        for (int i = 0; i < _model.CorePlayerModels.Length; i++) {
            if (i == player) {
                continue;
            }

            if (_model.CoreFieldModel.Cells[cellAfter, i] <= 0) {
                continue;
            }

            _model.CoreFieldModel.Cells[0, i] += _model.CoreFieldModel.Cells[cellAfter, i];
            _model.CoreFieldModel.Cells[cellAfter, i] = 0;
            _model.CorePlayerModels[i].KillPawnsInCell(cellAfter);
        }
    }

    public List<int> GetAvailablePawnToMove(int playerIndex, int throwRes) {
        List<int> res = new List<int>();
        CorePlayerModel player = _model.CorePlayerModels[playerIndex];
        for (int index = 0; index < player.Pawns.Length; index++) {
            int pawnPos = player.Pawns[index];
            if (CheckAvailableMove(pawnPos, throwRes)) {
                res.Add(index);
            }
        }

        return res;
    }

    private bool CheckAvailableMove(int from, int throwRes) {
        if (from == 0 && throwRes != 6) {
            return false;
        }

        if (from + throwRes >= _pathLength) {
            return false;
        }

        return true;
    }
}