using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float slideModifier;
    public float slideLength;
    public float jumpForce;
    public float sprintModifier;
    private float gravity = 20f;
    private float verticalVelocity;
    private Renderer rend;
    private CharacterController controller;
    public Camera MainCamera;
    private float baseFOV;
    private Vector3 Origin;
    private float sprintFOVMod = 1.25f;
    private bool Sliding;
    public float slideTime;
    private float speed = 15f;
    private Vector3 slide_dir;


    void Start()
    {
        
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        controller = GetComponent<CharacterController>();
        baseFOV = MainCamera.fieldOfView;
        Origin = MainCamera.transform.localPosition;
    }

    void Update()
    {

        float deltaX = Input.GetAxisRaw("Horizontal");
        float deltaZ = Input.GetAxisRaw("Vertical");

        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool slide = Input.GetKeyDown(KeyCode.LeftControl);

        bool isJumping = jump && !controller.isGrounded;
        bool isSprinting = sprint && deltaZ > 0 && !isJumping && controller.isGrounded;
        bool isSliding = isSprinting && slide && !Sliding;

        Vector3 movement = Vector3.zero;
        float AdjustedSpeed = speed;

        if (!Sliding)
        {
            movement = new Vector3(deltaX, 0, deltaZ);
            //movement = Vector3.ClampMagnitude(movement, speed); //limits speed
            movement.Normalize();
            movement = transform.TransformDirection(movement);

            if (isSprinting) AdjustedSpeed *= sprintModifier;
        }
        else
        {
            movement = slide_dir;
            AdjustedSpeed *= slideModifier;
            slideTime -= Time.deltaTime;
            if (slideTime <= 0)
            {
                Sliding = false;
            }
        }

        movement.y = verticalVelocity / AdjustedSpeed; //applies gravity
        movement = movement * AdjustedSpeed;
        controller.Move(movement * Time.deltaTime);


        if (isSliding)
        {
            Sliding = true;
            slide_dir = movement;
            slideTime = slideLength;
        }

        if (!controller.isGrounded && Sliding)
        {
            GameObject.Find("Main Camera").GetComponent<Viewbob>().enabled = false;

        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<Viewbob>().enabled = true;

        }

        if (controller.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump") || Input.GetButton("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;

        }

        if (Sliding)
        {
            MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, baseFOV * sprintFOVMod * 0.5f, Time.unscaledDeltaTime * 10f);
            MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, Origin + Vector3.down * 15f, Time.unscaledDeltaTime * 6f);
        }
        
        if (isSprinting)
        {
            MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, baseFOV * sprintFOVMod, Time.unscaledDeltaTime * 10f);
        }
        else if (!Input.GetMouseButton(1))
        {
            MainCamera.fieldOfView = Mathf.Lerp(MainCamera.fieldOfView, baseFOV, Time.unscaledDeltaTime * 10f);
            MainCamera.transform.localPosition = Vector3.Lerp(MainCamera.transform.localPosition, Origin, Time.unscaledDeltaTime * 6f);
        }



    }
}