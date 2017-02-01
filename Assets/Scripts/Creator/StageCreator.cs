using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageCreator : MonoBehaviour {

    public InputField widthText;
    public InputField heightText;

    List<GameObject> field;
    int width, height;

	// Use this for initialization
	void Start () {
        width = int.Parse(widthText.text);
        height = int.Parse(heightText.text);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
