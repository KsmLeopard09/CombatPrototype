using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEvade : StateMachineBehaviour
{
    public string boolName;

    // This method is called when the animator exits the state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, false);
    }
}
