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
    int vertialCount;

    public void Ctor() {
        btnSize = 22;
        Img_Smile.gameObject.SetActive(true);
        Img_Defeat.gameObject.SetActive(false);
    }

    public void Init(int horizontalCount, int vertialCount, int mineCount) {

        this.horizontalCount = horizontalCount;
        this.vertialCount = vertialCount;

        // 尺寸更新
        int width = horizontalCount * btnSize;
        int height = vertialCount * btnSize;
        // TopGroup
        Vector2 topGroupSize = topGruop.GetComponent<RectTransform>().sizeDelta;
        topGroupSize = new Vector2(width, topGroupSize.y);
        topGruop.GetComponent<RectTransform>().sizeDelta = topGroupSize;


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


        int btnCount = horizontalCount * vertialCount;
        // 生成Horizontal Line
        for (int i = 0; i < horizontalCount; i++) {
            GameObject.Instantiate(prefab_Horizontal_Line, horizontalLine_Group);
        }

        // 生成Vertical Line
        for (int i = 0; i < vertialCount; i++) {
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

        // 生成雷
        int[] mines = new int[mineCount];
        int addCount = 0;
        while (addCount < mineCount) {
            int id = UnityEngine.Random.Range(0, btnCount - 1);
            if (Array.Exists<int>(mines, value => value == id)) {
                continue;
            }
            mines[addCount] = id;
            addCount++;
        }

        // 设置雷
        for (int i = 0; i < mineCount; i++) {
            var id = mines[i];
            allElement.TryGetValue(id, out var element);
            element.hasMine = true;
            element.img_mineTrue.gameObject.SetActive(true);
        }

        // 设置数字
        for (int i = 0; i < btnCount; i++) {
            var element = allElement[i];
            if (element.hasMine) {
                continue;
            }
            TryAddCenterCount(GetUp(i, out var ele1), ele1, element);
            TryAddCenterCount(GetUpLeft(i, out var ele2), ele2, element);
            TryAddCenterCount(GetUpRight(i, out var ele3), ele3, element);
            TryAddCenterCount(GetLeft(i, out var ele4), ele4, element);
            TryAddCenterCount(GetRight(i, btnCount, out var ele5), ele5, element);
            TryAddCenterCount(getDownLeft(i, btnCount, out var ele6), ele6, element);
            TryAddCenterCount(GetDown(i, btnCount, out var ele7), ele7, element);
            TryAddCenterCount(GetdownRight(i, btnCount, out var ele8), ele8, element);
            if (element.centerCount == 0) {
                element.centetCountTxt.GetComponent<Text>().text = "";
            } else {
                element.centetCountTxt.GetComponent<Text>().text = element.centerCount.ToString();
            }
        }

    }

    public void UpdateMine(int id) {
        var element = allElement[id];
        element.Open();
        if (element.centerCount == 0) {
            TryOpenAroundBtn(id);
        }
    }

    public void TryOpenAroundBtn(int id) {
        for (int x = -1; x <= 1; x += 1) {
            for (int y = -1; y <= 1; y += 1) {
                // 8 times
                if (x == 0 && y == 0) {
                    continue;
                }
                int neighborX = GetX(id) + x;
                int neighborY = GetY(id) + y;
                if (neighborX < 0 || neighborX >= horizontalCount) {
                    continue;
                }
                if (neighborY < 0 || neighborY >= vertialCount) {
                    continue;
                }
                int neighborIndex = GetIndex(neighborX, neighborY);
                var ele = allElement[neighborIndex];
                TryOpenBtn(true, ele);
            }
        }
        // TryOpenBtn(GetUp(id, out var ele1), ele1);
        // TryOpenBtn(GetUpLeft(id, out var ele2), ele2);
        // TryOpenBtn(GetUpRight(id, out var ele3), ele3);
        // TryOpenBtn(GetLeft(id, out var ele4), ele4);
        // TryOpenBtn(GetRight(id, allElement.Count, out var ele5), ele5);
        // TryOpenBtn(GetDown(id, allElement.Count, out var ele6), ele6);
        // TryOpenBtn(GetdownRight(id, allElement.Count, out var ele7), ele7);
        // TryOpenBtn(getDownLeft(id, allElement.Count, out var ele8), ele8);
    }
    void TryOpenBtn(bool hasEle, Panel_InGameElement ele) {
        // 揭开按钮
        if (hasEle) {
            if (ele.hasMine) {
                return;
            }
            bool isOpened = ele.isOpened;
            if (!isOpened) {
                ele.Open();
                if (ele.centerCount == 0) {
                    TryOpenAroundBtn(ele.id);
                }
            }
        }
    }

    void TryAddCenterCount(bool hasEle, Panel_InGameElement element, Panel_InGameElement center) {
        if (hasEle) {
            if (element.hasMine) {
                center.centerCount++;
            }
        }
    }


    public bool IsWin() {
        for (int i = 0; i < allElement.Count; i++) {
            var ele = allElement[i];
            if (ele.hasMine) {
                continue;
            }
            if (ele.isOpened == false) {
                return false;
            }
        }
        return true;
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
