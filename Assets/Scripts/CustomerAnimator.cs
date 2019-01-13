using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CustomerAnimator : MonoBehaviour
{
    Animator animator;
    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void SetKneeling(bool state)
    {
        animator.SetBool("Kneeling", state);
    }
}
