using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platform;
    public GameObject fence;
    public GameObject pole;
    public GameObject player;
    public GameObject cam;
    public float worldSize;
    public Vector3 platformSize; 
    public Vector3 playerStart;

    void Start()
    {
        worldSize = 12;
        float a = worldSize / 2;
        platformSize = new Vector3(worldSize, 0.5f, worldSize);
        playerStart = new Vector3(1f, 1, 1f);
        //Instantiates the predefined platform prefab at the (0,0,0) position:
        GameObject pf = Instantiate(platform, new Vector3(a,0,a), Quaternion.identity);
        //sets the prefab of the platform to a desired size:
        pf.transform.localScale = platformSize;
        SpawnPlayer();
        SpawnFences();
    }

    void SpawnPlayer()
    {   
        GameObject playr = Instantiate(player, playerStart, Quaternion.identity);
        GameObject camera = Instantiate(cam, new Vector3(playerStart.x, playerStart.y, playerStart.z), Quaternion.identity);        //first-person, egocentric
        //GameObject camera = Instantiate(cam, new Vector3(5, 15, 5), Quaternion.identity);                                               //third-person, allocentric
        //camera.transform.rotation = Quaternion.Euler(90,0,0);                                                                           //third-person, allocentric
        camera.transform.SetParent(playr.transform);                                                                                //first-person, egocentric
    }
    void SpawnFences()
    {
        for (int i = 0; i < worldSize; i++)
        {
            Instantiate(fence, new Vector3(0, 0, i), Quaternion.identity);
            Instantiate(fence, new Vector3(i, 0, 0), Quaternion.identity);
            Instantiate(fence, new Vector3(worldSize, 0, i), Quaternion.identity);
            Instantiate(fence, new Vector3(i, 0, worldSize), Quaternion.identity);
        }
        Instantiate(fence, new Vector3(worldSize, 0, worldSize), Quaternion.identity); //fixes that one random border piece that's missing
        for (int i = 0; i < worldSize; i++)
        {
            for(int j = 1; j< worldSize; j++)
            {
                if (i % 4 == 0)
                {
                    Instantiate(fence, new Vector3(i, 0, j + 1), Quaternion.identity);
                }
                else if (i % 2 == 0)
                {
                    Instantiate(fence, new Vector3(i, 0, j - 1), Quaternion.identity);
                }
            }
        }


    }
}
