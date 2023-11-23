using UnityEngine;

/*
This is attached to each non-player vehicle and manages their behavior.
Has global vars for velocityForce, carRb, and a var to call mainManager's instance.
Has methods for initializing vars, managing movement and out of bounds, and to have position be reset.
*/
public class EnemyVehicleController : MonoBehaviour
{
    //Variables
    [SerializeField] float velocityForce;

    MainManager mainManager;

    Rigidbody carRb;

    // Start is called before the first frame update
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        mainManager = MainManager.Instance;
    }

    // Update is called once per frame
    //Manages movement and destruction out of bounds.
    void FixedUpdate()
    {
        if (!mainManager.isGamePaused)
        {
            carRb.AddForce(transform.forward * velocityForce);
        }

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    //The reset function resets the car's movement and places it at input.
    //Not currently used.
    /*
    public void Reset(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        carRb.velocity = Vector3.zero;
        carRb.angularVelocity = Vector3.zero;
    }
    */


}
