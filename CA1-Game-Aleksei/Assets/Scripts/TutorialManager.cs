using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    public int popUpIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        if (popUpIndex == 0)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
            if (Input.anyKeyDown)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 7)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                popUpIndex++;
            }
        }
        

    }
}
