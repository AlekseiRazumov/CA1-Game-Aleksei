using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public bool frozen = false;
    private Rigidbody2D rig2D;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Unfreeze()
    {
        if (frozen)
        {
            
            frozen = false;
            rig2D.constraints = RigidbodyConstraints2D.None;
            rig2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
       
        
    }
    public void Freeze()
    {
       
            frozen = true;
            rig2D.constraints = RigidbodyConstraints2D.FreezeAll;
        
    }

}
