using UnityEngine;

public static class GameBusiness {

    public static void EnterGame(GameContext ctx, int horizontalCount, int vertialCount, int mineCount) {
        GridDomain.SetGrid(ctx, horizontalCount, vertialCount, mineCount);
        UIDomain.Panel_InGame_Open(ctx, horizontalCount, vertialCount, mineCount);
        UIDomain.Panel_Init(ctx, ctx.gridRepo);
        ctx.fsmCom.EnterInGame();
    }

    public static void Tick(GameContext ctx, float dt) {
        var fsm = ctx.fsmCom;
        ref var time = ref ctx.uiCtx.panel_InGame.timeValue;
        if (fsm.status == GameStatus.Ingame) {
            if (fsm.isEnteringGame) {
                fsm.isEnteringGame = false;
            }
            time += dt;
            UIDomain.Panel_InGame_SetTime(ctx, time);

            // 点击鼠标右键竖旗
            if (Input.GetMouseButtonDown(1)) {
                bool has = UIDomain.FindNearlyButton(ctx, Input.mousePosition, out var ele);
                if (has) {
                    if (ele.isFlaged) {
                        ele.FlagClose();
                        return;
                    }
                    ele.FlagShow();
                }

            }

        } else if (fsm.status == GameStatus.GameEnd) {
            if (fsm.isEnteringGameEnd) {
                fsm.isEnteringGameEnd = false;
                UIDomain.Panel_Result_Open(ctx, fsm.isDefeat);
            }
        }
    }
}