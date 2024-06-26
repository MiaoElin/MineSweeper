using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Panel_InGame : MonoBehaviour {

    // TopGroup
    [SerializeField] Transform topGruop;
    [SerializeField] Text mineCountTxt;
    [SerializeField] Image Img_Smile;
    [SerializeField] Image Img_Defeat;
    [SerializeField] Text timeTxt;

    public float timeValue;

    // BgGroup
    [SerializeField] Transform BgGruop;
    [SerializeField] Image prefab_Vertical_Line;
    [SerializeField] Image prefab_Horizontal_Line;
    [SerializeField] Transform vertialLine_Group;
    [SerializeField] Transform horizontalLine_Group;

    // BtnGroup
    [SerializeField] Transform btnGroup;
    [SerializeField] Panel_InGameElement prefab_btn;
    Dictionary<int, Panel_InGameElement> allElement;
    public int btnSize;
    public Action<int, bool> onBtnClickHandle;

    int horizontalCount; // 几格
    int verticalCount;


    public void Ctor() {
        timeValue = 0;
        btnSize = 22;
        Img_Smile.gameObject.SetActive(true);
        Img_Defeat.gameObject.SetActive(false);
    }

    public void SetTime_Tick(float value) {
        timeTxt.GetComponent<Text>().text = "Time:" + value.ToString("F1");
    }
    public void Init(int horizontalCount, int verticalCount, int mineCount) {

        this.horizontalCount = horizontalCount;
        this.verticalCount = verticalCount;

        // 尺寸更新
        int width = horizontalCount * btnSize;
        int height = verticalCount * btnSize;
        // TopGroup
        Vector2 topGroupSize = topGruop.GetComponent<RectTransform>().sizeDelta;
        topGroupSize = new Vector2(width, topGroupSize.y);
        topGruop.GetComponent<RectTransform>().sizeDelta = topGroupSize;
        mineCountTxt.GetComponent<Text>().text = "MineCount:" + mineCount.ToString();



        // Bg
        RectTransform bgGroupRect = BgGruop.GetComponent<RectTransform>();
        Vector2 bgGroupSize = bgGroupRect.sizeDelta;
        bgGroupSize = new Vector2(width, height);
        bgGroupRect.sizeDelta = bgGroupSize;

        // Prefab_Horizontal_line
        RectTransform prefab_Hor_Line = prefab_Horizontal_Line.GetComponent<RectTransform>();
        Vector2 hor_LineSize = prefab_Hor_Line.sizeDelta;
        hor_LineSize = new Vector2(2, height);
        prefab_Hor_Line.sizeDelta = hor_LineSize;

        // Prefab_Vertical_line
        RectTransform prefab_Ver_Line = prefab_Vertical_Line.GetComponent<RectTransform>();
        Vector2 ver_LineSize = prefab_Ver_Line.sizeDelta;
        ver_LineSize = new Vector2(width, 2);
        prefab_Ver_Line.sizeDelta = ver_LineSize;

        // BtnGroup
        RectTransform btnGroupRect = btnGroup.GetComponent<RectTransform>();
        Vector2 btnGroupSize = btnGroupRect.sizeDelta;
        btnGroupSize = new Vector2(width, height);
        btnGroupRect.sizeDelta = btnGroupSize;


        int btnCount = horizontalCount * verticalCount;
        // 生成Horizontal Line
        for (int i = 0; i < horizontalCount; i++) {
            GameObject.Instantiate(prefab_Horizontal_Line, horizontalLine_Group);
        }

        // 生成Vertical Line
        for (int i = 0; i < verticalCount; i++) {
            GameObject.Instantiate(prefab_Vertical_Line, vertialLine_Group);
        }

        // 生成Btn
        allElement = new Dictionary<int, Panel_InGameElement>();
        for (int i = 0; i < btnCount; i++) {
            Panel_InGameElement element = GameObject.Instantiate(prefab_btn, btnGroup);
            element.Ctor(i);
            element.btn.onClick.AddListener(() => {
                onBtnClickHandle.Invoke(element.id, element.hasMine);
            });
            // element.btn.gameObject.SetActive(false);
            var color = element.btn.GetComponent<Image>().color;
            color.a = 0.5f;
            element.btn.GetComponent<Image>().color = color;
            allElement.Add(i, element);
        }
    }

    internal void GetMineBrok(int id) {
        var ele = allElement[id];
        ele.img_mineFalse.gameObject.SetActive(true);
        ele.img_mineTrue.gameObject.SetActive(false);
    }

    internal void OpenAllBtn() {
        for (int i = 0; i < allElement.Count; i++) {
            var ele = allElement[i];
            if (ele.isOpened) {
                continue;
            }
            ele.Open();
        }
    }

    public void Init(int id, int centerCount, bool hasMine) {
        var ele = allElement[id];
        ele.centerCount = centerCount;
        if (centerCount == 0) {
            ele.centetCountTxt.GetComponent<Text>().text = "";
        } else {
            ele.centetCountTxt.GetComponent<Text>().text = centerCount.ToString();
        }

        if (hasMine) {
            ele.MineTrueShow();
        }

    }
    public void UpdateElements(int id, int centerCount, bool hasMine, bool isFlaged, bool isOpened) {
        var ele = allElement[id];

        ele.hasMine = hasMine;

        if (isFlaged) {
        } else {
            ele.FlagClose();
        }
        if (isOpened) {
            ele.Open();
        }
    }



    bool GetUp(int id, out Panel_InGameElement element) {
        int i = id - 16;
        if (i >= 0) {
            element = allElement[i];
            return true;
        }
        element = null;
        return false;
    }

    int GetIndex(int x, int y) {
        return y * horizontalCount + x;
    }

    int GetX(int index) {
        return index % horizontalCount;
    }

    int GetY(int index) {
        return index / horizontalCount;
    }

    bool GetUpLeft(int centerIndex, out Panel_InGameElement element) {
        element = null;
        int x = GetX(centerIndex);
        x -= 1;
        if (x < 0) {
            return false;
        }

        int y = GetY(centerIndex);
        y -= 1;
        if (y < 0) {
            return false;
        }

        int index = GetIndex(x, y);
        element = allElement[index];
        return true;
    }
    bool GetUpRight(int id, out Panel_InGameElement element) {
        int i = id - 17;
        if (i >= 0) {
            element = allElement[i];
            return true;
        }
        element = null;
        return false;
    }


    bool GetRight(int id, int btnCount, out Panel_InGameElement element) {
        int i = id + 1;
        if (i < horizontalCount) {
            element = allElement[i];
            return true;
        }
        element = null;
        return false;
    }

    bool GetLeft(int id, out Panel_InGameElement element) {
        int i = id - 1;
        if (i >= 0) {
            element = allElement[i];
            return true;
        }
        element = null;
        return false;
    }
    bool GetDown(int id, int btnCount, out Panel_InGameElement element) {
        int i = id + 16;
        if (i < btnCount) {
            element = allElement[i];
            return true;
        }
        element = null;
        return false;
    }

    bool getDownLeft(int id, int btnCount, out Panel_InGameElement element) {
        int i = id + 15;
        if (i < btnCount) {
            element = allElement[i];
            return true;
        }
        element = null;
        return false;
    }

    bool GetdownRight(int id, int btnCount, out Panel_InGameElement element) {
        int i = id + 17;
        if (i < btnCount) {
            element = allElement[i];
            return true;
        }
        element = null;
        return false;
    }

    public bool FindNearlyButton(Vector2 mousePos, out Panel_InGameElement element) {
        element = null;
        float neaarlyDistance = btnSize / 2 * btnSize / 2;
        for (int i = 0; i < allElement.Count; i++) {
            var ele = allElement[i];
            if (ele.isOpened) {
                continue;
            }
            float distanceCurrent = Vector2.SqrMagnitude(mousePos - (Vector2)ele.transform.position);
            if (distanceCurrent <= neaarlyDistance) {
                neaarlyDistance = distanceCurrent;
                element = ele;
            }
        }
        if (element == null) {
            return false;
        } else {
            return true;
        }
    }

    public void Close() {
        Destroy(gameObject);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

}
