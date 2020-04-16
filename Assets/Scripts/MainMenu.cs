using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    static public int mazeSize;
    static public int POV;
    static public string ID;

    public void StartGame()
    {
        SceneManager.LoadScene("MazeBasic");
    }
    public void GetSize(string text)
    {
        mazeSize = int.Parse(text);
    }
    public void GetPOV(int option)
    {
        Debug.Log(option);
        POV = option;
    }
    public void GetID(string id)
    {
        ID = id;
    }
}
