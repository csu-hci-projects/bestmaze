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
    static public int mazesPerBlock;
    static public int testTrials;
    static public int timeOut;
    static public string trialType; //learning: D, testing: T, survey: S

    public void DisplayConsent()
    {
        trialType = "D";
        SceneManager.LoadScene("Consent");
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
        if(id == null)
        {
            ID = "0";
        }
        else
        {
            ID = id;
        }
    }
    public void setCues(string num)
    {
        cues = int.Parse(num);
    }

    public void setMazesPerBlock(string num)
    {
        mazesPerBlock = int.Parse(num);
    }

    public void setTestTrials(string num)
    {
        testTrials = int.Parse(num);
    }

    public void setTimeOut(string num)
    {
        timeOut = int.Parse(num);
    }
}
