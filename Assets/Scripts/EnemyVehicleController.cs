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
        //carRb.AddForce(Vector3.forward * velocityForce);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
