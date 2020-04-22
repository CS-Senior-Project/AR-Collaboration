
using UnityEngine;

public class collidercheck : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        Physics.IgnoreCollision(GetComponent<Collider>(),player.GetComponent<Collider>());
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("!!!laser destroy"+collision.collider.name);
    }
}
