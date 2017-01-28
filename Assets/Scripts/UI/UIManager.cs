using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;

    public GameObject info;

    void Awake() {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InfoText(string text) {
        info.SetActive(true);
        var textBox = info.transform.GetChild(0).GetComponent<Text>();
        var anime = info.GetComponent<Animator>();
        textBox.text = text;
        anime.SetTrigger("In");
    }
}
