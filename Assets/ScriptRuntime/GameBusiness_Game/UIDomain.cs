using UnityEngine;

public static class UIDomain {

    public static void Panel_InGame_Open(GameContext ctx, int horizontalCount, int vertialCount, int mineCount) {
        UIApp.Panel_InGame_Open(ctx.uiCtx, horizontalCount, vertialCount, mineCount);
    }

    public static void Panel_InGame_Hide(GameContext ctx) {
        UIApp.Panel_InGame_Hide(ctx.uiCtx);
    }

    public static void Panel_InGame_UpdateMine(GameContext ctx, int id) {
        UIApp.Panel_InGame_UpdateMine(ctx.uiCtx, id);
    }

    public static void Panel_InGame_DefeatUpdate(GameContext ctx, int id) {
        // todo
        // 点的雷变红
        // 将所有按钮翻开
    }

    public static bool Panel_InGame_IsWin(GameContext ctx) {
        return UIApp.Panel_InGame_IsWin(ctx.uiCtx);
    }
    public static void Panel_Result_Open(GameContext ctx, bool hasMine) {
        if (hasMine) {
            // 失败页
        } else {
            // 胜利页
        }
    }

}