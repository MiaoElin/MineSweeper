using System;
using UnityEngine;

public static class UIApp {

    public static void Panel_InGame_Open(UIContext ctx, int horizontalCount, int vertialCount, int mineCount) {
        ctx.panel_InGame.Ctor();
        ctx.panel_InGame.Init(horizontalCount, vertialCount, mineCount);
    }

}