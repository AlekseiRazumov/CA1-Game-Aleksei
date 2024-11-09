using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rig2D;
    // Start is called before the first frame update
    void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rig2D.AddForce(direction * force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cube")
        {
            collision.GetComponent<CubeController>().Freeze();
        }
        Destroy(gameObject);
    }
}
