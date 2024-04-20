using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float rotSpeed;
    public float rotSmoothTime;
    public CharacterController controller;
    public float gravityForce;
    public Animator animator;

    public bool isChopping;
    public NavMeshAgent agent;
    public AudioSource snowFootstep;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
        PlayerWalkAudio();
    }

    public void PlayerWalkAudio()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            snowFootstep.enabled = true;
        }
        else
        {
            snowFootstep.enabled = false;
        }
    }
    public void PlayerControl()
    {
        //move horizontal, x axis// when we press A feedbacks -x,D +x
        //move vertical , y axis// when we press W feedbacks +y,S -y
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //movement = Vector 3 (X,Y,Z)
        //Normalize , when we press W and D together, makes normalize x and z return single value
        Vector3 movement = new Vector3(-moveHorizontal,0,moveVertical).normalized;
        //Animation Handler
        //movement.magnitude , is when the vector 3 has a value
        float moveMagnitude = movement.magnitude;
        animator.SetFloat("Speed", moveMagnitude);

        //GravityHandler
        if (!controller.isGrounded)
        {
            movement.y -= gravityForce * Time.deltaTime;
        }
        else
        {
            movement.y = 0f;
        }

        //Rotation handler
        if (movement.magnitude == 0f) return;
            // Calculate target angle in degrees
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            // Clamp the target angle to prevent unwanted rotations
            targetAngle = Mathf.Clamp(targetAngle, -180, 180); // Adjust limits if needed
            // Smoothly damp the angle towards the target
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotSpeed, rotSmoothTime);
            // Update only the y-axis rotation (assuming movement controls facing direction)
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        //MovementHandler
        controller.Move(movement * walkSpeed * Time.deltaTime);
    }


 


}

