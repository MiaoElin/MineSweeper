using UnityEngine;

public class GameContext {
    public GameFSMComponent fsmCom;

    public UIContext uiCtx;
    public GridRepo gridRepo;

    public GameContext() {
        fsmCom = new GameFSMComponent();
        gridRepo = new GridRepo();
    }

    public void Inject(UIContext uiCtx) {
        this.uiCtx = uiCtx;
    }

}