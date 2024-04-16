using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public Camera Camera;
    public float Speed;
    public Animator Animator;
    public Harvester Harvester;

    private void FixedUpdate()
    {
        Movement();
        Animations();
    }

  
    private void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = Camera.transform.TransformDirection(direction);
        direction.y = 0;
        direction.Normalize();
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Rigidbody.rotation = lookRotation;
        }

        Vector3 movementV = direction * Speed * Time.fixedDeltaTime;
        Rigidbody.velocity = movementV;
    }
    private void Animations()
    {
        if (IsMove())
        {
            if (Harvester.HasProducts())
            {
                Animator.Play("CharacterArmature|Run_Carry");
            }
            else
            {
                Animator.Play("CharacterArmature|Run");
            }
            
        }
        else
        {
            if (Harvester.HasProducts())
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
        return Rigidbody.velocity != Vector3.zero;
    }

}
