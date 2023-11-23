using UnityEngine;

public class PropOutOfBounds : MonoBehaviour
{
    [SerializeField] float outOfBoundsY;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= outOfBoundsY)
        {
            Destroy(gameObject);
        }
    }
}
