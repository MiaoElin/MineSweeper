using UnityEngine;

public class MainContext {
    public UIContext uiCtx;
    public GameContext gamCtx;
    public UIEventCenter eventCenter;
    public InputEntity input;

    public MainContext() {
        input = new InputEntity();
        uiCtx = new UIContext();
        eventCenter = new UIEventCenter();
        gamCtx = new GameContext();
    }

    public void Inject(Canvas screenCanvas) {
        uiCtx.Inject(screenCanvas, eventCenter, input);
        gamCtx.Inject(uiCtx);
    }
}