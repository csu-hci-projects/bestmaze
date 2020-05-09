using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FillerButtons : MonoBehaviour
{
    string gender = "";
    int hour = 0;
    string[] genders = { "Female", "Male", "Other", "Choose not to identify" };
    public void LoadIntro()
    {
        SceneManager.LoadScene("IntroTrial");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MazeBasic");
    }

    public void GetGender(int option)
    {
        Debug.Log("Dropdown option: " + option);
        Debug.Log("Dropdown variable: " + genders[option]);
        gender = genders[option];
    }

    public void GetHours(string hours)
    {
        hour = int.Parse(hours);
    }
    
    public void endMaze()
    {
        using (StreamWriter sw = File.AppendText(Application.dataPath + Spawner.path))
        {
            //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
            sw.WriteLine(MainMenu.ID + ",S," + Spawner.attemptNumber + ", Survey Data" + ",N/A" + "," + AudioCue.currentlyPlaying + "," + Time.time + "," + gender + "," + hour);
            sw.Close();
        }
        SceneManager.LoadScene("EndGame");
    }

    public void endGame()
    {
        Application.Quit();
    }
}
