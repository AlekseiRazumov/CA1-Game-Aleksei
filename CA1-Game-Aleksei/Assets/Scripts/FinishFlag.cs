using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    public bool isFinished = false;
    [SerializeField] TutorialManager tutorialManager;
    //finish the game if player touches the flag
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isFinished)
        {
            tutorialManager.popUpIndex++;
            isFinished = true;
        }
    }
}
