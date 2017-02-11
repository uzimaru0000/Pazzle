using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageCreator : MonoBehaviour {

    [System.Serializable]
    public enum PlaceType {
        Floor,
        Cube,
        Obj,
        Goal
    }

    public GameObject floor;
    public GameObject wall;
    public GameObject cube;
    public GameObject obj;
    public GameObject goal;
    public GameObject pointer;
    public InputField widthText;
    public InputField heightText;

    GameObject[,] field;
    GameObject pointerInstance;
    int width, height;
    PosiData offset {
        get {
            return new PosiData {
                x = width / 2,
                y = height / 2
            };
        }
    }
    PosiData selectPoint;
    public PlaceType type;

    void Start () {
        width = 1;
        height = 1;
        field = new GameObject[width + 2, height + 2];
        CreateField();

        widthText.onEndEdit.AddListener((s) => {
            try {
                width = int.Parse(s);
            } catch {
                widthText.text = "1";
                width = 1;
            }
            if (width == 0) {
                widthText.text = "1";
                width = 1;
            }
            CreateField();
        });

        heightText.onEndEdit.AddListener((s) => {
            try {
                height = int.Parse(s);
            } catch {
                heightText.text = "1";
                height = 1;
            }
            if (height == 0) {
                heightText.text = "1";
                height = 1;
            }
            CreateField();
        });
    }

    void CreateField() {
        foreach (var c in field) {
            if (c) Destroy(c);
        }
        field = new GameObject[width + 2, height + 2];
        for (var x = -1; x < width + 1; x++) {
            for (var y = -1; y < height + 1; y++) {
                var px = x - offset.x;
                var py = y - offset.y;
                GameObject obj;
                if ((x >= 0 && x < width) && (y >= 0 && y < height)) {
                    obj = Create(floor, new Vector3(px, 0, py), Quaternion.Euler(90, 0, 0));
                } else {
                    obj = Instantiate(wall, new Vector3(px, 0, py), Quaternion.Euler(90, 0, 0));
                }
                field[x + 1, y + 1] = obj;
            }
        }
        if (pointerInstance) Destroy(pointerInstance);
    }

    public void setPointer(Transform target) {
        if (!pointerInstance) {
            pointerInstance = Instantiate(pointer, target.position, Quaternion.identity);
        }
        pointerInstance.transform.position = target.position;
        selectPoint = new PosiData {
            x = (int) target.position.x,
            y = (int) target.position.z
        };
    }

    public void setType(string t) {
        type = (PlaceType) Enum.Parse(typeof(PlaceType), t, true);
    }

    public void Replace() {
        var px = selectPoint.x + offset.x + 1;
        var py = selectPoint.y + offset.y + 1;
        Destroy(field[px, py]);
        GameObject go = null;
        switch (type) {
            case PlaceType.Floor:
                go = Create(floor, new Vector3(selectPoint.x, 0, selectPoint.y), Quaternion.Euler(90, 0, 0));
                break;
            case PlaceType.Goal:
                go = Create(goal, new Vector3(selectPoint.x, 0.1f, selectPoint.y), Quaternion.Euler(90, 0, 0));
                break;
            case PlaceType.Cube:
                go = Create(cube, new Vector3(selectPoint.x, 0.5f, selectPoint.y), Quaternion.identity);
                break;
            case PlaceType.Obj:
                go = Create(obj, new Vector3(selectPoint.x, 0.5f, selectPoint.y), Quaternion.identity);
                break;
        }
        field[px, py] = go;
    }

    GameObject Create(GameObject prefab, Vector3 pos, Quaternion rotate) {
        GameObject obj;
        obj = Instantiate(prefab, pos, rotate);
        var events = obj.GetComponent<EventTrigger>();
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener(e => setPointer(obj.transform));
        events.triggers.Add(entry);
        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(e => Replace());
        events.triggers.Add(entry);
        return obj;
    }
}
