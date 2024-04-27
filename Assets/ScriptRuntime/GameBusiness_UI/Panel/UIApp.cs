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
            Debug.Log(value.name);
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
            Debug.Log(has);
            panel = GameObject.Instantiate<GameObject>(prefab, ctx.screenCanvas.transform).GetComponent<Panel_InGame>();
            panel.Ctor();
        }
        panel.Init(horizontalCount, vertialCount, mineCount);
        panel.onBtnClickHandle = () => { };
        panel.Show();
    }

    public static void Panel_InGame_Tick(UIContext ctx) {

    }

    public static void Panel_Hide(UIContext ctx) {
        Panel_InGame panel = ctx.panel_InGame;
        panel?.Hide();
    }

}