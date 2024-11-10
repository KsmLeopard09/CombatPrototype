using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyManage : MonoBehaviour
{
    Animator m_animator;
    GameObject parentObject;
    private void Start()
    {
        m_animator = GetComponent<Animator>();
        parentObject = gameObject.transform.parent.gameObject;
    }
    public void EndHeavyStart()
    {
        m_animator.SetBool("heavyAttack", false);
        m_animator.SetBool("attack", false);
    }
    public void EndHeavy()
    {
        m_animator.SetBool("heavyEnded", true);
    }
    private void OnAnimatorMove()
    {
        m_animator.ApplyBuiltinRootMotion();
        parentObject.transform.position += m_animator.deltaPosition;
        parentObject.transform.rotation = m_animator.deltaRotation;
    }
}
