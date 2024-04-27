using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_InGameElement : MonoBehaviour {

    [SerializeField] public Button btn;

    [SerializeField] public Image img_mineTrue;
    [SerializeField] public Image img_mineFalse;
    public int id;
    public int centerCount;
    public Text centetCountTxt;
    public bool hasMine;

    public bool isOpened;

    public void Ctor(int id) {
        this.id = id;
        centerCount = 0;
        hasMine = false;
        isOpened = false;
    }

    public void Open() {
        isOpened = true;
        btn.gameObject.SetActive(false);
    }

}