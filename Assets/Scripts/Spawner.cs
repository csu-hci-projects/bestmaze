using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public float worldSize;
    public Vector3 platformSize; 
    public Vector3 playerStart;
    public MazeGen mazeGen;
    //public MainMenu menu;
    public int perspective;
    public List<Material> colors;
    

    void Start()
    {
        //mazeGen = GameObject.Find("MazeGen").GetComponent<MazeGen>();

        Debug.Log(MainMenu.mazeSize);
        ArrayList mazeRaw = mazeGen.create(MainMenu.mazeSize);

        perspective = 1;    //egocentric: 0, allocentric: 1

        float a = (worldSize+1)/2;
        //platformSize = new Vector3(worldSize, 0.5f, worldSize);
        playerStart = new Vector3(1f, 0.5f, 1f);
        //Instantiates the predefined platform prefab at the (0,0,0) position:
        //GameObject pf = Instantiate(platform, new Vector3(a,0,a), Quaternion.identity);
        //sets the prefab of the platform to a desired size:
        //pf.transform.localScale = platformSize;
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
        GameObject playr = Instantiate(player, playerStart, Quaternion.identity);
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
    }
}
