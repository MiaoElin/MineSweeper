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
    Dictionary<int, Panel_InGameElement> allBtn;
    public int btnSize;
    public Action onBtnClickHandle;

    public void Ctor() {
        btnSize = 22;
        Img_Smile.gameObject.SetActive(true);
        Img_Defeat.gameObject.SetActive(false);
    }

    public void Init(int horizontalCount, int vertialCount, int mineCount) {
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
        allBtn = new Dictionary<int, Panel_InGameElement>();
        for (int i = 0; i < btnCount; i++) {
            Panel_InGameElement element = GameObject.Instantiate(prefab_btn, btnGroup);
            element.id = i;
            element.hasMine = false;
            element.btn.onClick.AddListener(() => {
                onBtnClickHandle.Invoke();
            });
            allBtn.Add(i, element);
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
            allBtn.TryGetValue(id, out var value);
            value.hasMine = true;
        }

    }

    // public void UpdateMine()

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
