using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceTip : MonoBehaviour
{
    [SerializeField] float forceAmount;
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
        Debug.Log($"{pos} and {rot}");
        transform.SetLocalPositionAndRotation(pos, rot);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Boink");
        if (other.GetComponent<Rigidbody>())
        {
            Debug.Log("Boink2");
            other.GetComponent<Rigidbody>().AddForce(transform.position.normalized * forceAmount, ForceMode.Impulse);
        }
    }
}
