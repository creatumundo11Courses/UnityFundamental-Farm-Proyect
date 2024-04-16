using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterIA : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform TargetT;
    public Animator Animator;
    public Shopper Shopper;


    private void FixedUpdate()
    {
        Animations();
    }

    private void Animations()
    {
        if (IsMove())
        {
            if (Shopper.HasProducts())
            {
                Animator.Play("CharacterArmature_Walk_Carry");
            }
            else 
            {
                Animator.Play("CharacterArmature|Walk");
            }

        }
        else
        {
            if (Shopper.HasProducts())
            {
                Animator.Play("CharacterArmature_Idle_Carry");
            }
            else
            {
                Animator.Play("CharacterArmature|Idle");
            }
        }
    }

    private bool IsMove()
    {
        return Agent.velocity.magnitude > 0;
    }

    private void Move(Vector3 destinationPos)
    {
        Agent.SetDestination(destinationPos);
    }

    public void MoveToTransform(Transform destinationT)
    {
        TargetT.position = destinationT.position;
        MoveTo();
    }
    private void MoveTo()
    {
        Move(TargetT.position);
    }

}
