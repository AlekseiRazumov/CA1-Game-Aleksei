using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    [SerializeField] GameObject door;
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
            position.y += 6f;
            door.transform.position = position;
            door.GetComponent<AudioSource>().Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            Vector2 position = door.transform.position;
            position.y -= 6f;
            door.transform.position = position;
            door.GetComponent<AudioSource>().Play();
        }
    }
}
