﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Obj {

	// Use this for initialization
	new void Start () {
        base.Start();
        StageManager.Instance.setGoal((int)transform.position.x, (int)transform.position.z);
	}
}
