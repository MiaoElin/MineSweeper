using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class UIContext {
    Dictionary<string, GameObject> UIs;
    public AsyncOperationHandle uiPtr;

    public Panel_InGame panel_InGame;

    public Canvas screenCanvas;
    public InputEntity input;
    public UIEventCenter eventCenter;

    public UIContext() {
        UIs = new Dictionary<string, GameObject>();
    }

    public void Inject(Canvas screenCanvas, UIEventCenter eventCenter, InputEntity input) {
        this.screenCanvas = screenCanvas;
        this.eventCenter = eventCenter;
        this.input = input;
    }

    public void UIsAdd(string name, GameObject value) {
        UIs.Add(name, value);
    }

    public bool UIsTryGetValue(string name, out GameObject value) {
        return UIs.TryGetValue(name, out value);
    }

}