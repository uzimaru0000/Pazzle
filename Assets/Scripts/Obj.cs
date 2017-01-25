using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StageManager.Instance.setObj((int)transform.position.x, (int)transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
