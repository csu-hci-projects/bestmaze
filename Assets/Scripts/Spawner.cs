using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using MazeGen;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platform;
    public GameObject fence;
    public GameObject player;
    public GameObject cam;
    public new GameObject light;
    private float worldSize = (int)MainMenu.mazeSize;
    public Vector3 platformSize; 
    public Vector3 playerStart;
    public MazeGen mazeGen;
    //public MainMenu menu;
    private int perspective = MainMenu.POV; //egocentric: 0, allocentric: 1
    public List<Material> colors;

    public GameObject pole;
    public GameObject audioCue;
    public GameObject playr;
    

    void Start()
    {
        //mazeGen = GameObject.Find("MazeGen").GetComponent<MazeGen>();
        if ((int)worldSize <= 0) worldSize = 10; //DEFAULT SIZE = 9
        ArrayList mazeRaw = mazeGen.create((int)worldSize);

        //float center = (worldSize+1)/2;
        playerStart = new Vector3(1f, 0.5f, 1f);
        SpawnPlatform(worldSize, mazeRaw);
        SpawnPlayer();
        SpawnFences(mazeRaw);
    }

    void SpawnPlatform(float worldSize, ArrayList maze)
    {
        for(int i = 0; i < worldSize; i++)
        {
            Material strip = colors[UnityEngine.Random.Range(0, colors.Count)];
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
        GameObject lighting = Instantiate(light, new Vector3(1f, 0.5f, 1f), Quaternion.identity);
        lighting.transform.SetParent(playr.transform);

        
        //first-person
        if (perspective == 0)
        {
            GameObject camera = Instantiate(cam, new Vector3(playerStart.x, playerStart.y, playerStart.z), Quaternion.identity);        //first-person, egocentric
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
        if (startingWall == 0)
        {
            int startingPosition = 0;
            while (maze[1].ToString()[startingPosition] != ' ')
            {
                startingPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            playr.transform.position = new Vector3(1, 0.5f, startingPosition);
            playr.transform.rotation = Quaternion.Euler(0, 90, 0);
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
            playr.transform.position = new Vector3(startingPosition, 0.5f, 1);
            playr.transform.rotation = Quaternion.Euler(0, 0, 0);
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
            playr.transform.position = new Vector3((int)worldSize - 1, 0.5f, startingPosition);
            playr.transform.rotation = Quaternion.Euler(0, 270, 0);
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
            playr.transform.position = new Vector3(startingPosition, 0.5f, (int)worldSize - 1);
            playr.transform.rotation = Quaternion.Euler(0, 180, 0);
            int exitPosition = 0;
            while (maze[exitPosition].ToString()[1] != ' ')
            {
                exitPosition = UnityEngine.Random.Range(1, (int)worldSize);
            }
            Instantiate(pole, new Vector3(exitPosition, 0, 1), Quaternion.identity);
        }

        int numAudioCues = 5;
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
