using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FillerButtons : MonoBehaviour
{

    public void LoadIntro()
    {
        SceneManager.LoadScene("IntroTrial");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MazeBasic");
    }
}
