using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVehicleController : MonoBehaviour
{
    //Variables
    [SerializeField] float speed = 15.0f;
    [SerializeField] float velocityForce;


    Rigidbody carRb;

    // Start is called before the first frame update
    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        carRb.AddForce(transform.forward * velocityForce);
        //transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    //The reset function resets the car's movement and places it at input.
    public void Reset(Vector3 pos, Quaternion rot){
        transform.SetPositionAndRotation(pos, rot);
        carRb.velocity = Vector3.zero;
        carRb.angularVelocity = Vector3.zero;
    }


}
