using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] PlayerController playerController;
    private float distance;
    
    //If cube is on the panel, open the door
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            Vector2 position = door.transform.position;
            distance = Vector2.Distance(playerController.transform.position,position);
            door.GetComponent<AudioSource>().volume = Mathf.Clamp(1-distance/20f,0f,1f);
            door.GetComponent<AudioSource>().Play();
            position.y += 6f;
            door.transform.position = position;
            
        }
    }
    //if cube isn't on the panel, close the door
    private void OnTriggerExit2D(Collider2D collision)
    {
        
            if (collision.gameObject.tag == "Cube" && playerController != null && door != null)
            {
                Vector2 position = door.transform.position;
                position.y -= 6f;
                door.transform.position = position;
            
           

        }
        
        
    }
}
