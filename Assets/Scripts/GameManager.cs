using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public GameState State;

    void Start() {
        Instance = this;
        State = GameState.play;
    }

    void LateUpdate() {
        if (StageManager.Instance.checkGoal()) {
            State = GameState.goal;
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