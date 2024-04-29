using System;
using UnityEngine;

public static class GridDomain {
    public static void SetGrid(GameContext ctx, int horizontalCount, int verticalCount, int mineCount) {
        ctx.gridRepo.SetGrid(horizontalCount, verticalCount, mineCount);
    }

    public static void UpdateMine(GameContext ctx, int id) {
        ctx.gridRepo.UpdateMine(id);
    }

    public static bool IsWin(GameContext ctx) {
        return ctx.gridRepo.IsWin();
    }
}
