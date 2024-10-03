using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyManage : MonoBehaviour
{
    Animator m_animator;
    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    public void EndHeavyStart()
    {
        m_animator.SetBool("heavyStarted", false);
        m_animator.SetBool("attack", false);
    }
    public void EndHeavy()
    {
        m_animator.SetBool("heavyEnded", true);
    }
}
