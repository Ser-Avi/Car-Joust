using UnityEngine;

/*
Should be attached to the lance tip gameobject.
Manages the lance tip's punching power.
Has vars for force amount, the player using the lance, and pos and rot info.
Has methods for initializing vars, 
*/
public class LanceTip : MonoBehaviour
{
    [SerializeField] float forceAmount;
    [SerializeField] GameObject player;

    Vector3 pos;
    Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        forceAmount = (MainManager.Instance.forceSetting/5)*forceAmount;
        pos = transform.localPosition;
        rot = transform.localRotation;
    }

    // This is needed so the tip stays in place. Load bearing code.
    void LateUpdate()
    {
        transform.SetLocalPositionAndRotation(pos, rot);
    }

    // OnTriggerEnter if the other has a rigidbody (this should be given I think) punch them away from player location.
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Vector3 forceDirection = (transform.position-player.transform.position).normalized;
            other.GetComponent<Rigidbody>().AddForce(forceDirection * forceAmount * other.GetComponent<Rigidbody>().mass,
                                     ForceMode.Impulse);
        }
    }
}
