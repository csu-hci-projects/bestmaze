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
    static public int cues;
    static public int audioPOV;
    


    public void StartGame()
    {
        SceneManager.LoadScene("MazeBasic");
    }
    public void GetSize(string text)
    {
        mazeSize = int.Parse(text);
        if(mazeSize%2==1)
            mazeSize += 1;
    }
    public void GetPOV(int option)
    {
        POV = option;
    }
    public void GetID(string id)
    {
        ID = id;
    }
    public void setCues(string num)
    {
        cues = int.Parse(num);
    }
    public static void setAudioPOV(int option)
    {
        // ego = 0, allo = 1, no cues = 2
        audioPOV = option;
    }
}
