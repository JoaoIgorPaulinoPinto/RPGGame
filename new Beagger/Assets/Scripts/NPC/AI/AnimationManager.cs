using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    NPCBehavior behavior;
    private void Start()
    {
        animator = GetComponent<Animator>();
        behavior = GetComponent<NPCBehavior>();
    }
    private void Update()
    {
        Vector3 direction = behavior.agent.velocity.normalized;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
        if(direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
