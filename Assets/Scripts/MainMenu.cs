using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    static public int mazeSize;

    public void StartGame()
    {
        SceneManager.LoadScene("MazeBasic");
    }
    public void MazeSize(int text)
    {
        mazeSize = text;
        
    }
}
