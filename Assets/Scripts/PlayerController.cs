using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    [SerializeField] float motorForce;
    [SerializeField] float breakForce;
    float currentBreakForce;
    [SerializeField] float maxSteerAngle = 30;
    float horizontalInput;
    float forwardInput;
    float steerAngle;

    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode cameraKey;

    public string inputID;

    [SerializeField] GameObject wheelFR;
    [SerializeField] GameObject wheelFL;
    [SerializeField] GameObject wheelBR;
    [SerializeField] GameObject wheelBL;

    [SerializeField] WheelCollider wheelFRCollider;
    [SerializeField] WheelCollider wheelFLCollider;
    [SerializeField] WheelCollider wheelBRCollider;
    [SerializeField] WheelCollider wheelBLCollider;

    [SerializeField] bool isBreaking;

    private Vector3 originPosition;
    private Quaternion originRotation;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //camera code
        if (Input.GetKeyDown(cameraKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }

        //reset vehicle location if it goes off screen
        if (transform.position.y < -5)
        {
            Respawn();
        }
    }

    //Manages physics updates
    void LateUpdate()
    {
        GetInput();
        MotorManager();
        SteeringManager();

    }

    //Manages Player input information
    private void GetInput()
    {
        //Axis Info
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        forwardInput = Input.GetAxis("Vertical" + inputID);

        //REPLACE cause 2 player
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    //Drives the wheels based on forward input. Manages breaking as well.
    private void MotorManager()
    {
        wheelFRCollider.motorTorque = forwardInput * motorForce;
        wheelFLCollider.motorTorque = forwardInput * motorForce;
        wheelBRCollider.motorTorque = forwardInput * motorForce;
        wheelBLCollider.motorTorque = forwardInput * motorForce;

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

/*
Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;

*/



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

    }
}