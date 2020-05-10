using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MazeExit : MonoBehaviour
{
    public GameObject target;
    void Start()
    {

    }

    //private void resetSolved()
    //{
    //    StreamWriter write = new StreamWriter("Assets/Scripts/Maze.txt", false);



    //    for (int i = 0; i < Spawner.mazeCopy.Count; i++)
    //    {
    //        write.WriteLine(Spawner.mazeCopy[i].ToString());
    //    }
    //    write.Close();
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioCue.play.Stop();
            //Debug.Log("attempt number: " + Spawner.attemptNumber);
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().StopCoroutine("Forward");

            if(AudioCue.perspective == 0)
            {
                AudioCue.perspective = AudioCue.perspective + 1;
                MainMenu.trialType = "D";
                Spawner.attemptNumber = 1;
                //regenerate maze
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Spawner.PracticeIntro.SetActive(false);
                Spawner.ExperimentIntro.SetActive(true);
                return;
            }

            if (Spawner.attemptNumber < (MainMenu.mazesPerBlock + MainMenu.testTrials))
            {
                MazeGen.mazeRaw = new ArrayList();
                for (int i = 0; i < Spawner.mazeCopy.Count; i++)
                {
                    MazeGen.mazeRaw.Add(Spawner.mazeCopy[i].ToString());
                }
                if (Spawner.attemptNumber < MainMenu.mazesPerBlock) //learning trials
                {
                    
                    MainMenu.trialType = "D";
                    Spawner.attemptNumber = Spawner.attemptNumber + 1;
                    //Debug.Log(Spawner.playerSpawn.x);
                    other.transform.position = new Vector3(Mathf.Round(Spawner.playerSpawn.x), other.transform.position.y, Mathf.Round(Spawner.playerSpawn.z));
                    //Debug.Log("original vector: " + Spawner.playerSpawn);
                    //Debug.Log("current vector: " + other.transform.position);
                    //other.transform.position = Spawner.playerSpawn;
                    other.transform.rotation = Spawner.playerRotation;
                    Spawner.LearningVictory.SetActive(true);
                }
                
                else //testing trials
                {
                    
                    MainMenu.trialType = "T";
                    
                    int randomX = 0;
                    int randomZ = 0;
                    //resetSolved();
                    while (Spawner.mazeCopy[randomX].ToString()[randomZ] != ' ')
                    {
                        randomX = UnityEngine.Random.Range(1, (int)Spawner.worldSize);
                        randomZ = UnityEngine.Random.Range(1, (int)Spawner.worldSize);
                    }
                    other.transform.position = new Vector3(Mathf.Round(randomX), other.transform.position.y, Mathf.Round(randomZ));
                    if (Spawner.attemptNumber == MainMenu.mazesPerBlock)
                    {
                        Spawner.TestIntro1.SetActive(true);
                    }
                    else
                    {
                        Spawner.TestIntro2.SetActive(true);
                    }
                    Spawner.attemptNumber = Spawner.attemptNumber + 1;
                }               
            }
            else
            {
                AudioCue.perspective = AudioCue.perspective + 1;
                Spawner.EndSection.SetActive(true);
                //if (AudioCue.perspective < 4)
                //{
                //    MainMenu.trialType = "D";
                //    Spawner.attemptNumber = 1;
                //    //regenerate maze
                //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //}
                //else
                //{
                //    SceneManager.LoadScene("Survey");
                //}
                
               
            }
        }
    }
}