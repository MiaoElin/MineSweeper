using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClientMain : MonoBehaviour/*, IPointerClickHandler*/ {

    [SerializeField] Canvas screenCanvas;
    public UnityEvent rightClick;
    MainContext ctx = new MainContext();
    void Start() {
        // Inject
        ctx.Inject(screenCanvas);

        // Load
        UIApp.Load(ctx.uiCtx);

        // Bind
        Bind(ctx);

        // 打开游戏页面
        GameBusiness.EnterGame(ctx.gamCtx, 16, 16, 20);

    }

    private void Bind(MainContext ctx) {
        var eventCenter = ctx.eventCenter;
        eventCenter.OnPanle_IngameBtnClickHandle += (int id, bool hasMine) => {
             // 有雷，游戏失败
            if (hasMine) {
                ctx.gamCtx.fsmCom.EnterGameEnd(hasMine);
                return;
            }
            // 无雷，翻开按钮，如果该按钮无数字，说明四周无雷打开八个方向的按钮，打开的里面有无数字的，继续打开8个方向的按钮
            
        };
    }

    // Update is called once per frame

    void Update() {

        // input
        ctx.input.Process();

        // GameBusinessTick
        GameBusiness.Tick(ctx.gamCtx);
    }
    // public void OnPointerClick(PointerEventData eventData) {
    //     if (eventData.button == PointerEventData.InputButton.Right) {
    //         rightClick.Invoke();
    //     }
    // }
    // private void ButtonRightClick() {
    //     Debug.LogError("Right Click");
    // }
}
