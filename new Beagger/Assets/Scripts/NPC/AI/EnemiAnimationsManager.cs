using UnityEngine.AI;
using UnityEngine;

public class EnemiAnimationsManager : MonoBehaviour
{
    Animator animator;
    NavMeshAgent behavior;
    private void Start()
    {
        animator = GetComponent<Animator>();
        behavior = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Vector3 direction = behavior.velocity.normalized;
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
        if (direction.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
