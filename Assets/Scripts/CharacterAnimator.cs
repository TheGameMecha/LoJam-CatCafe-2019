using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalk(bool state)
    {
        animator.SetBool("Walking", state);
    }

    public void SetFood(bool state)
    {
        animator.SetBool("HasFood", state);
    }

    public void InteractTrigger()
    {
        animator.SetTrigger("Interact");
    }
}
