using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager Instance;

    public GameObject wall;
    public GameObject floor;
    public int width;
    public int height;
    public float fadeTime;

    PosiData offset;
    FieldData[,] stageData;
    int movableCubeNum;

    public PosiData Offset { get; }

    void Awake() {
        Instance = this;
        stageData = new FieldData[width, height];
        offset = new PosiData { x = width / 2, y = height / 2 };
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                stageData[x, y] = new FieldData {
                    type = FieldData.ObjType.None,
                    onCube = false
                };
            }
        }

        for (var x = -1; x < width+1; x++) {
            for (var y = -1; y < height+1; y++) {
                var px = x - offset.x;
                var py = y - offset.y;
                GameObject obj;
                if ((x >= 0 && x < width) && (y >= 0 && y < height)) {
                    obj = Instantiate(floor, new Vector3(px, 0, py), Quaternion.Euler(90, 0, 0)) as GameObject;
                } else { 
                    obj = Instantiate(wall, new Vector3(px, 0, py), Quaternion.Euler(90, 0, 0)) as GameObject;
                }
                var rate = (x + y + 2.0f) / (width + height + 4.0f);
                obj.GetComponent<Obj>().interval = rate * fadeTime;
                obj.transform.SetParent(transform);
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

    public bool checkGoal() {
        int num = 0;
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                if (stageData[x, y].type == FieldData.ObjType.Goal && stageData[x, y].onCube) {
                    num++;
                }
            }
        }
        return movableCubeNum == num;
    }

    public void setObj(int x, int y) {
        stageData[x + offset.x, y + offset.y].type = FieldData.ObjType.Obj;
    }

    public void setGoal(int x, int y) {
        stageData[x + offset.x, y + offset.y].type = FieldData.ObjType.Goal;
    }

    public void setCube(int x, int y) {
        stageData[x + offset.x, y + offset.y].onCube = true;
        movableCubeNum++;
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
        Obj,
        Goal
    }

    public ObjType type;
    public bool onCube;
}