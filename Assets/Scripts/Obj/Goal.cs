using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Obj {

    public bool isGoal;

    // Use this for initialization
    new void Start() {
        base.Start();
        StageManager.Instance.setGoal((int)transform.position.x, (int)transform.position.z);
    }

    new void Update() {
        base.Update();
        if (isGoal) {
            var col = Color.Lerp(defaultColor, new Color(0, 2.5f, 20), timer);
            m_renderer.material.SetColor("_EmissionColor", col);
        }
    }

    public override void Clear() {
        isGoal = true;
        timer = 0;
    }

}
