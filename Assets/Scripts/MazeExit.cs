using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeExit : MonoBehaviour
{
    public GameObject target;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("attempt number: " + Spawner.attemptNumber);
            GameObject.Find("Player(Clone)").GetComponent<PlayerMovement>().StopCoroutine("Forward");
            
            if (Spawner.attemptNumber < (MainMenu.mazesPerBlock + MainMenu.testTrials))
            {
                
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
                }
                
                else //testing trials
                {
                    if (Spawner.attemptNumber == MainMenu.mazesPerBlock)
                    {
                        Spawner.TestAlert.SetActive(true);
                    }
                    MainMenu.trialType = "T";
                    Spawner.attemptNumber = Spawner.attemptNumber + 1;
                    int randomX = 0;
                    int randomZ = 0;
                    while (Spawner.mazeCopy[randomX].ToString()[randomZ] != ' ')
                    {
                        randomX = UnityEngine.Random.Range(1, (int)Spawner.worldSize);
                        randomZ = UnityEngine.Random.Range(1, (int)Spawner.worldSize);
                    }
                    other.transform.position = new Vector3(Mathf.Round(randomX), other.transform.position.y, Mathf.Round(randomZ));
                }               
            }
            else
            {
                AudioCue.perspective = AudioCue.perspective + 1;
                if (AudioCue.perspective == 3)
                {
                    //end of experiment
                    Debug.Log("end of experiment reached");
                }
                Spawner.attemptNumber = 1;
                //regenerate maze
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}