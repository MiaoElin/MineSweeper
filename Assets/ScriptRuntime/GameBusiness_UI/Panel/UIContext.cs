using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class UIContext {
    Dictionary<string, GameObject> UIs;
    public AsyncOperationHandle uiPtr;

    public Panel_InGame panel_InGame;

    public Canvas screenCanvas;
    public InputEntity input;

    public UIContext() {
        UIs = new Dictionary<string, GameObject>();
        input = new InputEntity();
    }

    public void Inject(Canvas screenCanvas) {
        this.screenCanvas = screenCanvas;
    }

    public void UIsAdd(string name, GameObject value) {
        UIs.Add(name, value);
    }

    public bool UIsTryGetValue(string name, out GameObject value) {
        Debug.Log(UIs.Count);
        return UIs.TryGetValue(name, out value);
    }

}