using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class UIApp {

    public static void Load(UIContext ctx) {
        string label = "UI";
        var ptr = Addressables.LoadAssetsAsync<GameObject>(label, null);
        var list = ptr.WaitForCompletion();
        foreach (var value in list) {
            ctx.UIsAdd(value.name, value);
        }
        ctx.uiPtr = ptr;
    }

    public static void Release(UIContext ctx) {
        if (ctx.uiPtr.IsValid()) {
            Addressables.Release(ctx.uiPtr);
        }
    }

    public static void Panel_InGame_Open(UIContext ctx, int horizontalCount, int vertialCount, int mineCount) {
        Panel_InGame panel = ctx.panel_InGame;
        if (panel == null) {
            bool has = ctx.UIsTryGetValue(typeof(Panel_InGame).Name, out var prefab);
            panel = GameObject.Instantiate<GameObject>(prefab, ctx.screenCanvas.transform).GetComponent<Panel_InGame>();
            panel.Ctor();
        }
        panel.Init(horizontalCount, vertialCount, mineCount);
        panel.onBtnClickHandle = (int id, bool hasMine) => { ctx.eventCenter.OnPanle_IngameBtnClick(id,hasMine); };
        panel.Show();
    }

    public static void Panel_InGame_UpdateMine(UIContext ctx, int id) {
        Panel_InGame panel = ctx.panel_InGame;
        panel?.UpdateMine(id);
    }

    public static void Panel_InGame_Hide(UIContext ctx) {
        Panel_InGame panel = ctx.panel_InGame;
        panel?.Hide();
    }

}