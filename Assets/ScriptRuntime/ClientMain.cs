using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClientMain : MonoBehaviour, IPointerClickHandler {
    // Start is called before the first frame update

    [SerializeField] Canvas screenCanvas;
    public UnityEvent rightClick;
    UIContext ctx = new UIContext();
    void Start() {
        Debug.Log("mine sweeper");
        ctx.Inject(screenCanvas);
        UIApp.Load(ctx);
        UIApp.Panel_InGame_Open(ctx, 16, 16, 20);
    }

    // Update is called once per frame

    void Update() {

        // input
        ctx.input.Process();

    }
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            rightClick.Invoke();
        }
    }
    private void ButtonRightClick() {
        Debug.LogError("Right Click");
    }
}
