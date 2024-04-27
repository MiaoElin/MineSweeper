using UnityEngine;

public class GameFSMComponent {

    public GameStatus status;

    public bool isEnteringGame;

    public bool isEnteringGameEnd;
    public bool isDefeat;


    public void EnterInGame() {
        status = GameStatus.Ingame;
        isEnteringGame = true;
    }

    public void EnterGameEnd(bool hasMine) {
        status = GameStatus.GameEnd;
        isEnteringGameEnd = true;
        isDefeat = hasMine;
    }
}