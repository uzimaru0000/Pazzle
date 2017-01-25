using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager Instance;

    public PosiData offset;
    public int width;
    public int height;

    FieldData[,] stageData;

    void Awake() {
        Instance = this;
        stageData = new FieldData[width, height];
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                stageData[x, y] = new FieldData {
                    type = FieldData.ObjType.None,
                    onCube = false
                };
            }
        }
    }
    void LateUpdate() {

    }

    public bool checkObj(int x, int y) {
        x += offset.x;
        y += offset.y;
        if (x < 0 || x >= width || y < 0 || y >= height) return true;
        return stageData[x, y].type == FieldData.ObjType.Obj;
    }

    public bool checkGoal(int x, int y) {
        return stageData[x + offset.x, y + offset.y].type == FieldData.ObjType.Goal;
    }

    public void setObj(int x, int y) {
        stageData[x + offset.x, y + offset.y].type = FieldData.ObjType.Obj;
    }

    public void setGoal(int x, int y) {
        stageData[x + offset.x, y + offset.y].type = FieldData.ObjType.Goal;
    }

    public void setCube(int x, int y) {
        stageData[x + offset.x, y + offset.y].onCube = true;
    }

    public bool MoveCheck(int x, int y, PosiData dir) {
        var dx = x + dir.x;
        var dy = y + dir.y;
        if (checkObj(dx, dy)) {
            return false;
        }
        if (stageData[dx + offset.x, dy + offset.y].onCube) {
            if (!MoveCheck(dx, dy, dir)) {
                return false;
            }
            return true;
        }
        return true;
    }

    public void Move(int x, int y, PosiData dir) {
        x += offset.x;
        y += offset.y;
        var dx = x + dir.x;
        var dy = y + dir.y;
        stageData[x, y].onCube = false;
        stageData[dx, dy].onCube = true;
    }

    void OnDestroy() {
        Instance = null;
    }
}

[System.Serializable]
public struct PosiData {
    public int x;
    public int y;
}

[System.Serializable]
public struct FieldData {
    public enum ObjType {
        None,
        MovableCube,
        Obj,
        Goal
    }

    public ObjType type;
    public bool onCube;
}