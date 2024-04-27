using UnityEngine;

public class GameContext {
    public GameFSMComponent fsmCom;

    public UIContext uiCtx;

    public GameContext() {
        fsmCom = new GameFSMComponent();
    }

    public void Inject(UIContext uiCtx) {
        this.uiCtx = uiCtx;
    }

}