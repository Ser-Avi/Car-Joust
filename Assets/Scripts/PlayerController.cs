using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    [SerializeField] float motorForce;
    [SerializeField] float breakForce;
    float currentBreakForce;
    [SerializeField] float maxSteerAngle = 30;
    float horizontalInput;
    float forwardInput;
    float steerAngle;

    //Camera
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode cameraKey;
    public string inputID;

    //Wheels
    [SerializeField] GameObject wheelFR;
    [SerializeField] GameObject wheelFL;
    [SerializeField] GameObject wheelBR;
    [SerializeField] GameObject wheelBL;

    [SerializeField] WheelCollider wheelFRCollider;
    [SerializeField] WheelCollider wheelFLCollider;
    [SerializeField] WheelCollider wheelBRCollider;
    [SerializeField] WheelCollider wheelBLCollider;

    [SerializeField] bool isBreaking;

    //Origin settings for Respawn
    private Vector3 originPosition;
    private Quaternion originRotation;

    //Center of Mass Management
    Rigidbody carRb;
    [SerializeField] GameObject centerOfMass;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        carRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    //Currently gets player input, switches cameras, and initiates respawn when driving offscreen.
    void Update()
    {
        GetInput();

        //Camera switching
        if (Input.GetKeyDown(cameraKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }

        //Reset vehicle location if it goes off screen or R gets pressed.
        //MOVE to game manager and tie this info to score system.
        if (transform.position.y < -5 || Input.GetKeyDown(KeyCode.R))
        {
            Respawn();
        }
    }

    /*
    Manages physics updates.
    Currently it calls the MotorManager for driving and the SteeringManager for steering.
    */
    void LateUpdate()
    {
        carRb.centerOfMass = centerOfMass.transform.localPosition;
        MotorManager();
        SteeringManager();
    }

    //Manages Player input information
    private void GetInput()
    {
        //Axis Info
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        forwardInput = Input.GetAxis("Vertical" + inputID);
    }

    //Drives the wheels based on forward input. Manages breaking as well.
    private void MotorManager()
    {

        wheelFRCollider.motorTorque = forwardInput * motorForce;
        wheelFLCollider.motorTorque = forwardInput * motorForce;
        wheelBRCollider.motorTorque = forwardInput * motorForce;
        wheelBLCollider.motorTorque = forwardInput * motorForce;

        isBreaking = wheelFRCollider.rpm * forwardInput < 0;    //breaks if we're trying to go in opposite direction than current

        currentBreakForce = isBreaking ? breakForce : 0;   //Breakforce set to 0 if breaking.
        ApplyBreaking();
    }

    //Manages the steering of the front two wheels based on sideways input.
    private void SteeringManager()
    {
        steerAngle = maxSteerAngle * horizontalInput;
        wheelFRCollider.steerAngle = steerAngle;
        wheelFLCollider.steerAngle = steerAngle;

        //Moving game object in the same direction as wheel collider.
        Quaternion rot;
        wheelFRCollider.GetWorldPose(out _, out rot);
        wheelFR.transform.rotation = rot;
        wheelFL.transform.rotation = rot;
    }

    //Applies breaking force to all wheels.
    private void ApplyBreaking()
    {
        wheelFRCollider.brakeTorque = currentBreakForce;
        wheelFLCollider.brakeTorque = currentBreakForce;
        wheelBRCollider.brakeTorque = currentBreakForce;
        wheelBLCollider.brakeTorque = currentBreakForce;
    }

    //Resets car to spawn position and status.
    private void Respawn()
    {
        transform.SetPositionAndRotation(originPosition, originRotation);
        carRb.velocity = Vector3.zero;
        carRb.angularVelocity = Vector3.zero;

        wheelFRCollider.rotationSpeed = 0;
        wheelFLCollider.rotationSpeed = 0;
        wheelBRCollider.rotationSpeed = 0;
        wheelBLCollider.rotationSpeed = 0;
    }
}