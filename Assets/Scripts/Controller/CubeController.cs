using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : Obj {
    
    public bool isMove;

    Animator anime;
    AudioSource audioSource;
    PosiData pData;
    bool goalAnime;

	// Use this for initialization
	new void Start () {
        base.Start();

        var sm = StageManager.Instance;
        anime = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        pData = new PosiData();
        pData.x = (int)transform.position.x;
        pData.y = (int)transform.position.y;
        sm.setCube(pData.x, pData.y);
	}
	
	// Update is called once per frame
	new void Update () {
        base.Update();
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
        if (goalAnime) {
            transform.position -= transform.up * Time.deltaTime;
            if (transform.position.y < -0.5f) goalAnime = false;
        }
	}

    bool Move(PosiData dir) {
        var sm = StageManager.Instance;
        var result = sm.MoveCheck(pData.x, pData.y, dir);
        if (result) sm.Move(pData.x, pData.y, dir);
        audioSource.Play();
        return result;
    }

    public override void Clear() {
        goalAnime = true;
    }

}
