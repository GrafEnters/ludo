using System.Collections.Generic;

public class CoreController {
    private Core _core;
    private CoreView _view;
    private int _throwRes;

    private string[] _hardcodePlayers = new[] { "George" };

    public void Subscribe(CoreView view) {
        _view = view;
        view.OnPlayerThrowDice += TryMove;
        view.OnPlayerSelectPawnToMove += MoveSelectedPawn;
    }

    public void StartCore() {
        _core = new Core();
        _core.SetUpModel(_hardcodePlayers);
        _view.PawnsViewController.CreatePawns(_core.PlayersAmount, _core.PawnsPerPlayer, OnPawnClicked);
    }

    private void TryMove(int throwRes) {
        _throwRes = throwRes;
        List<int> availableMoves = _core.GetAvailablePawnToMove(0, throwRes);
        if (availableMoves.Count == 1) {
            MoveSelectedPawn(availableMoves[0]);
        } else {
            _view.PawnsViewController.SetPawnsInteractive(availableMoves);
        }
    }

    private void OnPawnClicked(int owner, int index) {
        if (owner != 0) {
            return;
        }
        _view.PawnsViewController.ClearPawnsInteractive();
        MoveSelectedPawn(index);
    }

    public void MoveSelectedPawn(int selectedPawn) {
        _core.PlayerMakeMove(0, selectedPawn, _throwRes);
        _view.PawnsViewController.MovePawn(0, selectedPawn, _throwRes);
    }
}