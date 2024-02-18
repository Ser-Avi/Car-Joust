using System.Threading;
using UnityEngine;

/*
This is attached to each non-player vehicle and manages their behavior.
Has global vars for velocityForce, carRb, and a var to call mainManager's instance.
Has methods for initializing vars, managing movement and out of bounds, and to have position be reset.
*/
public class EnemyVehicleController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    MainManager mainManager;
    Rigidbody carRb;
    WheelCollider[] wheelColliders;
    public bool isMoving = true;
    float stuckTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = MainManager.Instance;
        carRb = GetComponent<Rigidbody>();
        wheelColliders = GetComponentsInChildren<WheelCollider>();
    }

    // Update is called once per frame
    //Manages movement and destruction out of bounds.
    void FixedUpdate()
    {
        if (!mainManager.isGamePaused)
        {
            DriveForward();
        }

        StuckManager();

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void DriveForward()
    {
        foreach (WheelCollider wheel in wheelColliders)
        {
            wheel.rotationSpeed = rotationSpeed;
        }
    }

/*
Destroys this game object if its velocity remains 0 for at least 3 seconds.
*/
    void StuckManager()
    {
        isMoving = carRb.velocity.magnitude > 0;

        if (isMoving)
        {
            stuckTime = -1;
        }

        if (!isMoving && stuckTime == -1)
        {
            stuckTime = Time.time;
        }
        else if (!isMoving && stuckTime + 3 < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
