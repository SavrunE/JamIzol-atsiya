using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour
{
    private bool isMoving = true;

    private NavMeshAgent agent;
    private Animator animator;
    private Mover mover;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Transform animationObject = GetComponentInChildren<Transform>().Find("Body");
        animator = animationObject.GetComponent<Animator>();

        mover = GetComponent<Mover>();

        isMoving = true;
    }

    void Update()
    {
        Vector3 normalizedMovement = agent.desiredVelocity.normalized;

        Vector3 forwardVector = Vector3.Project(normalizedMovement, transform.forward);
        Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);

        float forwardVelocity = forwardVector.magnitude * Vector3.Dot(forwardVector, transform.forward);
        float rightVelocity = rightVector.magnitude * Vector3.Dot(rightVector, transform.right);

        if ((forwardVelocity == 0 && rightVelocity == 0) || !mover.canMove)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("ForwardVelocity", forwardVelocity);
        animator.SetFloat("RightVelocity", rightVelocity);
    }
}
