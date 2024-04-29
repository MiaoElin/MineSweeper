using System;
using UnityEngine;
using UnityEngine.UI;

public class GridEntity {
    public int index;
    public int centerCount;
    public Text centetCountTxt;
    public bool hasMine;

    public bool isOpened;
    public bool isFlaged;


    public GridEntity() {

    }

    public void Ctor(int index) {
        this.index = index;
        centerCount = 0;
        hasMine = false;
        isOpened = false;
        isFlaged = false;
    }

    public void Init(int horizontalCount, int verticalCount) {

    }



}