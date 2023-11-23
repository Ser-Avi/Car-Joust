using UnityEngine;

/*
This allows an object to follow another one (generally the player).
Currently unused as camera gameObject is a child of the player.
Has vars for the player gameobject and a vector3 offset -- the vector of follower from the player.
*/
public class FollowPlayer : MonoBehaviour
{
    //vars
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position-player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //offset the camera behind the player, by adding to the player's position
        transform.position = player.transform.position + offset;
    }
}
