using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {
    
    public bool isMove;

    Animator anime;
    AudioSource audioSource;
    PosiData pData;

	// Use this for initialization
	void Start () {
        anime = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        pData = new PosiData();
        pData.x = (int)transform.position.x;
        pData.y = (int)transform.position.y;
        StageManager.Instance.setCube(pData.x, pData.y);
	}
	
	// Update is called once per frame
	void Update () {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var dir = new PosiData { x = (int)h,  y = (int)v };
        if (!isMove && GameManager.Instance.State == GameState.play) {
            if (h >= 1) {
                if (Move(dir)) {
                    anime.SetTrigger("Right");
                    pData.x++;
                } else {
                    anime.SetTrigger("RightCancel");
                }
                isMove = true;
            } else if (h <= -1) {
                if (Move(dir)) {
                    anime.SetTrigger("Left");
                    pData.x--;
                } else {
                    anime.SetTrigger("LeftCancel");
                }
                isMove = true;
            } else if (v >= 1) {
                if (Move(dir)) {
                    anime.SetTrigger("Front");
                    pData.y++;
                } else {
                    anime.SetTrigger("FrontCancel");
                }
                isMove = true;
            } else if (v <= -1) {
                if (Move(dir)) {
                    anime.SetTrigger("Back");
                    pData.y--;
                } else {
                    anime.SetTrigger("BackCancel");
                }
                isMove = true;
            }
        }
	}

    bool Move(PosiData dir) {
        var sm = StageManager.Instance;
        var result = sm.MoveCheck(pData.x, pData.y, dir);
        if (result) sm.Move(pData.x, pData.y, dir);
        return result;
    }
}
