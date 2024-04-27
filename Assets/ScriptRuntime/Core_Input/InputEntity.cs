using System;
using UnityEngine;

public class InputEntity {

    public bool isSetFlag;

    public InputEntity() {

    }

    public void Process() {
        isSetFlag = Input.GetMouseButtonDown(1);
    }
}