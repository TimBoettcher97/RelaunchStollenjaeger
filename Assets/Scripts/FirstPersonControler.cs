using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstPersonControler : MonoBehaviour
{
    [SerializeField]
    private CharacterController PlayerController;

    [SerializeField]
    private GameObject RotateVertically;

    [SerializeField]
    private float WalkingSpeed = 4f;

    [SerializeField]
    private float RunSpeed = 8f;

    [SerializeField] 
    [Range(0,1)] private float sidewaysMultiplier = .75f;

    [SerializeField] 
    [Range(0,1)] private float backwardsMultiplier = .5f;

    private Vector3 MoveSpeedPerDirection = new Vector3(4, 2, 1.5f);

    [SerializeField]
    private Vector2 CameraMax = new Vector2(60, 60);

    private float MovemendInputAtMoment_Horizontal;
    private float MovemendInputAtMoment_Vertical;

    public float mousSensitifity = 100f;
    private float MouseInputAtMoment_Horizontal;
    private float MouseInputAtMoment_Vertical;
    private float rotationY;

    public float jumpHeight = 1f;


    private float gravity = -9.81f;

    [Header("Gravitation")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;

    Vector3 velocity;
    bool isGrounded;
    public LayerMask groundMask;

   

    private void Start()
    {
        lastStep = transform.position;
        MoveSpeedPerDirection = new Vector3(WalkingSpeed, WalkingSpeed * sidewaysMultiplier, WalkingSpeed * backwardsMultiplier);
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }
        //if (Input.GetAxis("Horizontal") != 0)
        {
            MovemendInputAtMoment_Horizontal = Input.GetAxis("Horizontal");
        }
        //if (Input.GetAxis("Vertical") != 0)
        {
            MovemendInputAtMoment_Vertical = Input.GetAxis("Vertical");
        }

        MouseInputAtMoment_Horizontal = Input.GetAxis("Mouse X");
        MouseInputAtMoment_Vertical = Input.GetAxis("Mouse Y");

        if (isGrounded)
        {
            Footsteps();
        }

        CalculateMovement();
        CalculateMousRotation();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        PlayerController.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            MoveSpeedPerDirection = new Vector3(RunSpeed, RunSpeed * sidewaysMultiplier, RunSpeed * backwardsMultiplier);
            stepRange = 2;
        }
        else
        {
            MoveSpeedPerDirection= new Vector3(WalkingSpeed, WalkingSpeed * sidewaysMultiplier, WalkingSpeed * backwardsMultiplier);
            stepRange = 1.5f;
        }


    }
    
    [Header("Audio")]
    [SerializeField]
    private AudioManager audioManager;
    Vector3 lastStep;
    [SerializeField] float stepRange = 1.5f;
    public bool inCave;
    private void Footsteps()
    {
        if (Vector3.Distance(lastStep,transform.position) > stepRange)
        {
            lastStep = transform.position;
            if (inCave)            
            audioManager.Play("CaveStep");            
            else
            audioManager.Play("Step");
        }
    }


    private void CalculateMovement()
    {
        Vector3 movementVector = Vector3.zero;
        float calculatetHorizontalValue;
        float calculatetVerticalValue;

        //für leichteres Verstehen uwu
        float forwardSpeed = MoveSpeedPerDirection.x;
        float backwardSpeed = MoveSpeedPerDirection.z;
        float sidewaySpeed = MoveSpeedPerDirection.y;

        if (MovemendInputAtMoment_Vertical >= 0)
        {
            calculatetVerticalValue = forwardSpeed;
        }
        else
        {
            calculatetVerticalValue = backwardSpeed;
        }
        calculatetVerticalValue = calculatetVerticalValue * MovemendInputAtMoment_Vertical * Time.deltaTime;

        calculatetHorizontalValue = sidewaySpeed * MovemendInputAtMoment_Horizontal * Time.deltaTime;

        movementVector += PlayerController.transform.forward * calculatetVerticalValue;
        movementVector += PlayerController.transform.right * calculatetHorizontalValue;

        PlayerController.Move(movementVector);
    }

    private void CalculateMousRotation()
    {

        float rotationAroundY = MouseInputAtMoment_Horizontal * mousSensitifity * Time.deltaTime;
        float rotationAroundX = MouseInputAtMoment_Vertical * mousSensitifity * Time.deltaTime;


        PlayerController.transform.Rotate(Vector3.up * rotationAroundY);

        //rotationY += rotationAroundX;

        //rotationY = Mathf.Clamp(rotationY, CameraMax.x, CameraMax.y);
        //Quaternion xQuaternion = Quaternion.AngleAxis(rotationY, Vector3.right);
        //Quaternion originalRotation = PlayerCammera.transform.localRotation;
        //PlayerCammera.transform.localRotation = originalRotation * xQuaternion;

        //rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        rotationY += rotationAroundX;
        rotationY = Mathf.Clamp(rotationY, CameraMax.x, CameraMax.y);

        RotateVertically.transform.localEulerAngles = new Vector3(-rotationY, 0, 0); //change: transform.localEulerAngles.y --> 0 die Camera muss sich nicht um die y achse drehen, weil sie das schon macht, da sie ein child vom player ist
    }

    
}
