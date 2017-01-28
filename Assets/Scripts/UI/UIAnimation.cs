using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : StateMachineBehaviour {

    UIBaseScript ui;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ui = animator.GetComponent<UIBaseScript>();
        ui.isAnimation = true;
    }

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ui.isAnimation = false;
        ui.gameObject.SetActive(false);
	}
}
