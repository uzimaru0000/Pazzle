using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelMove : StateMachineBehaviour {

    public Vector3 direction;

    Transform transform;
    Vector3 startPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        transform = animator.transform;
        startPos = transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        transform.position = startPos + direction * -Mathf.Sin(stateInfo.normalizedTime * Mathf.PI);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var CCon = transform.GetComponent<CubeController>();
        transform.position = startPos;
        CCon.isMove = false;
    }

}
