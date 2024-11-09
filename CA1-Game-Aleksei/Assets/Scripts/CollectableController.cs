using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && this.gameObject.tag=="Battery")
        {
            Destroy(this.gameObject);
            playerController.AddBattery();
        }
        else if (collision.gameObject.tag == "Player" && this.gameObject.tag == "PowerUp")
        {
            Destroy(this.gameObject);
            playerController.AddPowerUp();
        }
    }
}
