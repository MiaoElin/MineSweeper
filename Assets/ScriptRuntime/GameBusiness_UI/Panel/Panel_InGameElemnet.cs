using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_InGameElement : MonoBehaviour {

    [SerializeField] public Button btn;

    [SerializeField] Image img_mineTrue;
    [SerializeField] Image img_mineFalse;
    public int id;
    public bool hasMine;

    public void Ctor() {

    }

}