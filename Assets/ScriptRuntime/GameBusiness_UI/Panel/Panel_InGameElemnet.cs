using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_InGameElement : MonoBehaviour {

    [SerializeField] public Button btn;

    [SerializeField] public Image img_mineTrue;
    [SerializeField] public Image img_mineFalse;
    [SerializeField] public Image img_Flag;
    public bool isFlaged;

    public int id;
    public int centerCount;
    public Text centetCountTxt;
    public bool hasMine;

    public bool isOpened;

    public void Ctor(int id) {
        this.id = id;
        centerCount = 0;
        hasMine = false;
        FlagClose();
        Close();
    }

    public void Open() {
        isOpened = true;
        btn.gameObject.SetActive(false);
    }

    public void Close() {
        isOpened = false;
        btn.gameObject.SetActive(true);
    }

    public void FlagShow() {
        isFlaged = true;
        img_Flag.gameObject.SetActive(true);
    }

    public void FlagClose() {
        isFlaged = false;
        img_Flag.gameObject.SetActive(false);
    }

    public void MineTrueShow() {
        hasMine = true;
        img_mineTrue.gameObject.SetActive(true);
    }

    public void MineFalseShow() {
        hasMine = true;
        img_mineTrue.gameObject.SetActive(false);
        img_mineFalse.gameObject.SetActive(true);
    }
}