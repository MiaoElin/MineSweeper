using UnityEngine;

public static class GameBusiness {

    public static void EnterGame(GameContext ctx, int horizontalCount, int vertialCount, int mineCount) {
        UIDomain.Panel_InGame_Open(ctx, horizontalCount, vertialCount, mineCount);
        ctx.fsmCom.EnterInGame();
    }

    public static void Tick(GameContext ctx) {
        var fsm = ctx.fsmCom;
        if (fsm.status == GameStatus.Ingame) {
            if (fsm.isEnteringGame) {
                fsm.isEnteringGame = false;
            }

        } else if (fsm.status == GameStatus.GameEnd) {
            if (fsm.isEnteringGameEnd) {
                fsm.isEnteringGameEnd = false;
                UIDomain.Panel_Result_Open(ctx, fsm.isDefeat);
            }
        }
    }
}