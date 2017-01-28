using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    GameState state;
    Blur _blurEffect;

    Blur BlurEffect { 
        get {
            if (!_blurEffect) {
                _blurEffect = Camera.main.GetComponent<Blur>();
            }
            return _blurEffect;
        }
    }

    public GameState State { 
        get {
            return state;
        }
        set {
            state = value;
            switch (state) {
                case GameState.pause:
                    BlurEffect.enabled = true;
                    break;
                case GameState.stanby:
                    UIManager.Instance.InfoText("Ready...");
                    break;
                case GameState.play:
                    BlurEffect.enabled = false;
                    break;
                case GameState.goal:
                    UIManager.Instance.InfoText("Clear!!");
                    foreach (var obj in FindObjectsOfType<Obj>()) {
                        obj.Clear();
                    }
                    break;
            }
        }
    }

    void Awake() {
        Instance = this;
    }

    void LateUpdate() {
        switch (state) {
            case GameState.pause:

                break;
            case GameState.stanby:
                if (!UIManager.Instance.info.GetComponent<UIBaseScript>().isAnimation) State = GameState.play;
                break;
            case GameState.play:
                bool flag = false;
                foreach (var cube in FindObjectsOfType<CubeController>()) {
                    flag = cube.isMove;
                }
                if (!flag && StageManager.Instance.checkGoal()) State = GameState.goal;
                break;
            case GameState.goal:
                
                break;
        }
    }

    void OnDestroy() {
        Instance = null;
    }
}

public enum GameState {
    pause,
    play,
    goal,
    stanby
}