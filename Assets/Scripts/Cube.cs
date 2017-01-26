using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Obj {

	// Use this for initialization
	new void Start () {
        base.Start();
        StageManager.Instance.setObj((int)transform.position.x, (int)transform.position.z);
	}

}
