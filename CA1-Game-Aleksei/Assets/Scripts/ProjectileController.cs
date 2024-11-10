using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rig2D;
    
    void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    //Launching the projectile
    public void Launch(Vector2 direction, float force)
    {
        rig2D.AddForce(direction * force);
    }
    //if the projectile hits cube, then freeze it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Cube")
        {
            collision.GetComponent<CubeController>().Freeze();
        }
        Destroy(gameObject);
    }
}
