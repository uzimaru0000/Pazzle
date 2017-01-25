using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StageManager.Instance.setGoal((int)transform.position.x, (int)transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
