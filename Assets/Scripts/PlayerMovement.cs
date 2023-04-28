using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody rb;
    private Vector3 moveVector;
    public Animator anim;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        Move();
    }

    public void Move()
    {
        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * moveSpeed * Time.deltaTime;
        moveVector.z = joystick.Vertical * moveSpeed * Time.deltaTime;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction =
                Vector3.RotateTowards(transform.forward, moveVector, rotationSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(direction);

            if (StackSystem.Instance.stackedPapers.Count > 0)
            {
                anim.SetBool("carryingRun",true);
                anim.SetBool("walk", false);    
            }
            else
            {
                anim.SetBool("walk", true);    
                anim.SetBool("carryingRun",false);
            }
           
        }
        else if (joystick.Horizontal == 0 || joystick.Vertical == 0)
        {
            if (StackSystem.Instance.stackedPapers.Count > 0)
            {
                anim.SetBool("carryingRun",false);
                anim.SetBool("carryingIdle",true);
            }
            else
            {
                anim.SetBool("walk", false);
                anim.SetBool("carryingIdle",false);
            }
            
        }
        
        rb.MovePosition(rb.position + moveVector);
    }
}
