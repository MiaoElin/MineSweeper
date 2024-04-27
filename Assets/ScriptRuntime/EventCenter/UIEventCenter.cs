using System;
using UnityEngine;

public class UIEventCenter {

    public Action<int,bool> OnPanle_IngameBtnClickHandle;
    public void OnPanle_IngameBtnClick(int id,bool hasMine) { OnPanle_IngameBtnClickHandle.Invoke(id,hasMine); }
}