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
    private UnitComponent unit;
    void Start()
    {
        unit = GetComponent<UnitComponent>();
        agent = GetComponent<NavMeshAgent>();

        Transform body = GetComponentInChildren<Transform>().Find("Body");
        animator = body.GetComponent<Animator>();

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

        if ((forwardVelocity == 0 && rightVelocity == 0) || !mover.CanMove)
        {
            isMoving = false;
            Dialog();
        }
        else
        {
            isMoving = true;

            VelocityAnimation(forwardVelocity, rightVelocity);
        }

        animator.SetBool("IsMoving", isMoving);
    }
    public void Dialog()
    {
        //смотреть на собеседничка и передавать 
        //VelocityAnimation(forwardVelocity, rightVelocity)
    }
    public void VelocityAnimation(float forward, float right)
    {
        animator.SetFloat("ForwardVelocity", forward);
        animator.SetFloat("RightVelocity", right);
    }
}
