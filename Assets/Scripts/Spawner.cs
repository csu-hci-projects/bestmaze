using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
//using MazeGen;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platform;
    public GameObject fence;
    public GameObject player;
    public GameObject cam;
    public GameObject lights;
    public static float worldSize = (int)MainMenu.mazeSize;
    public Vector3 platformSize; 
    public Vector3 playerStart;
    public MazeGen mazeGen;
    //public MainMenu menu;
    public int perspective = MainMenu.POV; //egocentric: 0, allocentric: 1
    public List<Material> colors;
    public GameObject pole;
    public GameObject audioCue;
    public GameObject playr;
    public static string path = "/Data Output/ParticipantData_ID" + MainMenu.ID + "_POV" + MainMenu.POV + "_mazeSize" + MainMenu.mazeSize + ".csv";
    public static List<int> audioCueBank;
    public static int attemptNumber;
    public static Vector3 playerSpawn;
    public static ArrayList mazeRaw;
    public static Quaternion playerRotation;
    public static ArrayList mazeCopy;
    public static GameObject ExperimentIntro;
    public static GameObject PracticeIntro;
    public static GameObject Survey;
    public static GameObject LearningVictory;
    public static GameObject TestIntro1;
    public static GameObject TestIntro2;
    public static GameObject EndSection;
    public static string currentAudioType;
    public static string[] audioTypes;

    void Start()
    {
        if ((int)worldSize <= 0) worldSize = 10;                                                       
        ArrayList mazeRaw = mazeGen.create((int)worldSize);
        CreateCSV();
        //float center = (worldSize+1)/2;
        playerStart = new Vector3(1f, 0.7f, 1f);
        SpawnPlatform(worldSize, mazeRaw);
        SpawnPlayer();
        //mazeCopy = mazeRaw;
        mazeCopy = new ArrayList();
        for (int i = 0; i < mazeRaw.Count; i++)
        {
            mazeCopy.Add(mazeRaw[i].ToString());
        }
        SpawnFences(mazeRaw);
        audioCueBank = new List<int>();
        audioCueBank.Clear();
        attemptNumber = 1;
        audioTypes = new string[] {"practice", "egocentric", "allocentric", "none"};
        currentAudioType = audioTypes[AudioCue.perspective];
        Survey = GameObject.Find("Survey");
        Survey.SetActive(false);
        ExperimentIntro = GameObject.Find("ExperimentIntro");
        ExperimentIntro.SetActive(false);
        LearningVictory = GameObject.Find("LearningVictory");
        LearningVictory.SetActive(false);
        TestIntro1 = GameObject.Find("TestIntro1");
        TestIntro1.SetActive(false);
        TestIntro2 = GameObject.Find("TestIntro2");
        TestIntro2.SetActive(false);
        EndSection = GameObject.Find("EndSection");
        EndSection.SetActive(false);
        PracticeIntro = GameObject.Find("PracticeIntro");
        PracticeIntro.SetActive(false);
        if (AudioCue.perspective == 0)
        {
            PracticeIntro.SetActive(true);
        }
        if (AudioCue.perspective == 1)
        {
            ExperimentIntro.SetActive(true);
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("Menu");
    }

    public void hidePracticeIntro()
    {
        PracticeIntro.SetActive(false);
    }
    public void hideExperimentIntro()
    {
        ExperimentIntro.SetActive(false);
    }
    public void hideLearningVictory()
    {
        LearningVictory.SetActive(false);
    }
    public void hideTestIntro1()
    {
        TestIntro1.SetActive(false);
    }
    public void hideTestIntro2()
    {
        TestIntro2.SetActive(false);
    }
    public void hideEndSection()
    {
        EndSection.SetActive(false);
        if (AudioCue.perspective < 4)
        {
            MainMenu.trialType = "D";
            Spawner.attemptNumber = 1;
            //regenerate maze
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene("Survey");
        }

    }

    void CreateCSV()
    {
        if (!System.IO.File.Exists(Application.dataPath + path))
        {
            StreamWriter sw = new StreamWriter(Application.dataPath + path, true);
            sw.WriteLine("ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,CurrentAudioType,Gender,VideoGame");
            sw.Close();
        }
    }

    void SpawnPlatform(float worldSize, ArrayList maze)
    {
        Material strip = colors[0];
        for (int i = 0; i < worldSize; i++)
        {
            if (i % 2 == 0)
            {
                strip = colors[UnityEngine.Random.Range(0, colors.Count)];
            }
            for (int j = 0; j < worldSize; j++)
            {
                GameObject floor = Instantiate(platform, new Vector3(i, 0, j), Quaternion.identity);
                floor.GetComponent<MeshRenderer>().material = strip;
            }
        }
    }


    void SpawnPlayer()
    {   
        playr = Instantiate(player, playerStart, Quaternion.identity);
        GameObject lighting = Instantiate(lights, new Vector3(1f, 0.7f, 1f), Quaternion.identity);
        lighting.transform.SetParent(playr.transform);

        
        //first-person
        if (perspective == 0)
        {
            GameObject camera = Instantiate(cam, playerStart, Quaternion.identity);                                                     //first-person, egocentric
            camera.transform.SetParent(playr.transform);                                                                                //first-person, egocentric
        }

        //third-person
        if (perspective == 1)
        {
            GameObject camera = Instantiate(cam, new Vector3(worldSize / 2, worldSize, worldSize / 2), Quaternion.identity);            //third-person, allocentric
            camera.transform.rotation = Quaternion.Euler(90, 0, 0);                                                                     //third-person, allocentric
        }
        
    }

    
    void SpawnFences(ArrayList maze)
    {
        for (int i = 0; i < maze.Count; i++)
        {
            for (int j = 0; j < maze[i].ToString().Length; j++)
            {
                if (maze[i].ToString()[j] == '#')
                {
                    Instantiate(fence, new Vector3(i, 0, j), Quaternion.identity);
                }
            }
        }


        int startingWall = UnityEngine.Random.Range(0, 4); //start at 0: left, 1: bottom, 2: right, 3: top
        int exitWall = UnityEngine.Random.Range(0, 4);
        while(startingWall == exitWall)
        {
            exitWall = UnityEngine.Random.Range(0, 4);
        }
        if (startingWall == 0)
        {
            int startingPosition = 0;
            while (maze[1].ToString()[startingPosition] != ' ')
            {
                startingPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            playr.transform.position = new Vector3(1, 0.7f, startingPosition);
            playerSpawn = playr.transform.position;
            playr.transform.rotation = Quaternion.Euler(0, 90, 0);
            playerRotation = Quaternion.Euler(0, 90, 0);
            int exitPosition = 0;
            while (maze[(int)worldSize - 1].ToString()[exitPosition] != ' ')
            {
                exitPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            Instantiate(pole, new Vector3((int)worldSize - 1, 0, exitPosition), Quaternion.identity);
        }
        if (startingWall == 1)
        {
            int startingPosition = 0;
            while (maze[startingPosition].ToString()[1] != ' ')
            {
                startingPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            playr.transform.position = new Vector3(startingPosition, 0.7f, 1);
            playerSpawn = playr.transform.position;
            playr.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerRotation = Quaternion.Euler(0, 0, 0);
            int exitPosition = 0;
            while (maze[exitPosition].ToString()[(int)worldSize - 1] != ' ')
            {
                exitPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            Instantiate(pole, new Vector3(exitPosition, 0, (int)worldSize - 1), Quaternion.identity);
        }
        if (startingWall == 2)
        {
            int startingPosition = 0;
            while (maze[(int)worldSize - 1].ToString()[startingPosition] != ' ')
            {
                startingPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            playr.transform.position = new Vector3((int)worldSize - 1, 0.7f, startingPosition);
            playerSpawn = playr.transform.position;
            playr.transform.rotation = Quaternion.Euler(0, 270, 0);
            playerRotation = Quaternion.Euler(0, 270, 0);
            int exitPosition = 0;
            while (maze[1].ToString()[exitPosition] != ' ')
            {
                exitPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            Instantiate(pole, new Vector3(1, 0, exitPosition), Quaternion.identity);
        }
        if (startingWall == 3)
        {
            int startingPosition = 0;
            while (maze[startingPosition].ToString()[(int)worldSize - 1] != ' ')
            {
                startingPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            playr.transform.position = new Vector3(startingPosition, 0.7f, (int)worldSize - 1);
            playerSpawn = playr.transform.position;
            playr.transform.rotation = Quaternion.Euler(0, 180, 0);
            playerRotation = Quaternion.Euler(0, 180, 0);
            int exitPosition = 0;
            while (maze[exitPosition].ToString()[1] != ' ')
            {
                exitPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            Instantiate(pole, new Vector3(exitPosition, 0, 1), Quaternion.identity);
        }

        int numAudioCues = MainMenu.cues;
        while (numAudioCues >= 0)
        {
            int audioCueSpawnX = 0;
            int audioCueSpawnY = 0;
            while (maze[audioCueSpawnX].ToString()[audioCueSpawnY] != ' ')
            {
                audioCueSpawnX = UnityEngine.Random.Range(1, (int)worldSize);
                audioCueSpawnY = UnityEngine.Random.Range(1, (int)worldSize);
            }
            Instantiate(audioCue, new Vector3(audioCueSpawnX, 0, audioCueSpawnY), Quaternion.identity);
            numAudioCues = numAudioCues - 1;
        }

    }
}
