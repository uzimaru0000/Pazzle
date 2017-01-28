using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : StateMachineBehaviour {

    public Vector3 direction = Vector3.zero;

    Transform transform;
    Vector3 startPos;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        transform = animator.transform;
        startPos = transform.position;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        transform.position = startPos + (stateInfo.normalizedTime * direction);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var CCon = transform.GetComponent<CubeController>();
        transform.position = startPos + direction;
        CCon.isMove = false;
	}

}
