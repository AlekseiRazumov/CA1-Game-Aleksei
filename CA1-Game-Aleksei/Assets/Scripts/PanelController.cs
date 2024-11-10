using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] PlayerController playerController;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != null && playerController!=null && door!=null)
        {
            if (collision.gameObject.tag == "Cube")
            {
                Vector2 position = door.transform.position;
                distance = Vector2.Distance(playerController.transform.position, position);
                door.GetComponent<AudioSource>().volume = Mathf.Clamp(1 - distance / 20f, 0f, 1f);
                door.GetComponent<AudioSource>().Play();
                position.y -= 6f;
                door.transform.position = position;

            }
        }
        
    }
}
