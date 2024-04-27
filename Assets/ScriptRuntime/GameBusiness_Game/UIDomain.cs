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
        UIApp.Panel_InGame_SetDefeatElement(ctx.uiCtx, id);
        // 将所有按钮翻开
        Panel_InGame_OpenAllBtn(ctx);
    }

    public static bool Panel_InGame_IsWin(GameContext ctx) {
        return UIApp.Panel_InGame_IsWin(ctx.uiCtx);
    }

    public static void Panel_InGame_OpenAllBtn(GameContext ctx) {
        UIApp.Panel_InGame_OpenAllBtn(ctx.uiCtx);
    }

    public static void Panel_InGame_SetTime(GameContext ctx, float value) {
        UIApp.Panel_InGame_SetTime(ctx.uiCtx, value);
    }

    public static bool FindNearlyButton(GameContext ctx, Vector2 mousePos, out Panel_InGameElement ele) {
        return UIApp.FindNearlyButton(ctx.uiCtx, mousePos, out ele);
    }
    public static void Panel_Result_Open(GameContext ctx, bool hasMine) {
        if (hasMine) {
            // 失败页
        } else {
            // 胜利页
        }
    }

}