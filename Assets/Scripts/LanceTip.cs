using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceTip : MonoBehaviour
{
    [SerializeField] float forceAmount;
    
    [SerializeField] GameObject player;
    Vector3 pos;
    Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.localPosition;
        rot = transform.localRotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.SetLocalPositionAndRotation(pos, rot);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boink");
        if (other.GetComponent<Rigidbody>())
        {
            Debug.Log("Boink2");
            Debug.Log(other.GetComponent<Rigidbody>().velocity.magnitude);
            Vector3 forceDirection = (transform.position-player.transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(forceDirection * forceAmount * other.GetComponent<Rigidbody>().mass,
                                     ForceMode.Impulse);
        }
    }
}
