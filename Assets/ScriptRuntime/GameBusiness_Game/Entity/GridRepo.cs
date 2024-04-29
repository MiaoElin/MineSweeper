using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class GridRepo {

    public List<GridEntity> allGrid;
    public int horizontalCount;
    public int verticalCount;

    public GridRepo() {
        allGrid = new List<GridEntity>();
    }
    public void SetGrid(int horizontalCount, int vertialCount, int mineCount) {

        int gridCount = horizontalCount * vertialCount;
        this.horizontalCount = horizontalCount;
        this.verticalCount = vertialCount;

        // 生成格子
        for (int i = 0; i < horizontalCount * vertialCount; i++) {
            GridEntity grid = new GridEntity();
            grid.Ctor(i);
            allGrid.Add(grid);
        }

        // 生成地雷
        int[] mines = new int[mineCount];
        int currentCount = 0;
        while (currentCount < mineCount) {
            int rd = UnityEngine.Random.Range(0, gridCount - 1);
            if (mines.Contains(rd)) {
                continue;
            }
            mines[currentCount] = rd;
            currentCount++;
            // 另一种
            // Array.Exists<int>(mines, value => value == rd);
        }

        // 设置地雷
        foreach (var id in mines) {
            allGrid[id].hasMine = true;
        }

        // 设置CenterCount
        for (int i = 0; i < gridCount; i++) {
            var grid = allGrid[i];
            if (grid.hasMine) {
                continue;
            }
            TrySetCenterCount(i, grid);
        }

    }

    public void Foreach(Action<GridEntity> action) {
        allGrid.ForEach(action);
    }


    private void TrySetCenterCount(int index, GridEntity centerGrid) {
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                int x = GetX(index) + i;
                int y = GetY(index) + j;
                if (x < 0 || x >= horizontalCount || y < 0 || y >= verticalCount) {
                    continue;
                }
                int newIndex = GetIndex(x, y);
                if (allGrid[newIndex].hasMine) {
                    centerGrid.centerCount++;
                }
            }
        }
    }

    int GetX(int index) {
        return index % horizontalCount;
    }

    int GetY(int index) {
        return index / horizontalCount;
    }

    int GetIndex(int x, int y) {
        return y * horizontalCount + x;
    }

    public bool IsWin() {
        for (int i = 0; i < allGrid.Count; i++) {
            var grid = allGrid[i];
            if (grid.hasMine) {
                continue;
            }
            if (grid.isOpened == false) {
                return false;
            }
        }
        return true;
    }

    public void UpdateMine(int id) {
        var grid = allGrid[id];
        grid.isOpened = true;
        if (grid.centerCount == 0) {
            TryOpenArroundGrid(id);
        }
    }

    private void TryOpenArroundGrid(int id) {
        for (int i = -1; i <= 1; i++) {
            for (int j = -1; j <= 1; j++) {
                int x = GetX(id) + i;
                int y = GetY(id) + j;
                if (x < 0 || x >= horizontalCount || y < 0 || y >= verticalCount) {
                    continue;
                }
                int index = GetIndex(x, y);
                var grid = allGrid[index];
                if (grid.isOpened) {
                    continue;
                }
                grid.isOpened = true;
                if (grid.centerCount == 0) {
                    TryOpenArroundGrid(index);
                }
            }
        }
    }
}