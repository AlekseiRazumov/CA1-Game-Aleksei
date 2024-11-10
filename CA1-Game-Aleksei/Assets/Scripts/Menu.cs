using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    //Managing menu with Play, Restart and Quit buttons
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Restart()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
