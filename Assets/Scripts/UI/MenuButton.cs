using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : UIBaseScript {

    public Text buttonText;
    public GameObject menu;

    bool isPause;

    public void Pause() {
        if (GameManager.Instance.State == GameState.stanby) return; 
        if (isPause) {
            GameManager.Instance.State = GameState.play;
            buttonText.text = "≡";
            menu.SetActive(false);
        } else { 
            GameManager.Instance.State = GameState.pause;
            buttonText.text = "=>";
            menu.SetActive(true);
        }
        isPause = !isPause;
    }
}