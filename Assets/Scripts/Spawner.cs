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
    public Vector3 platformSize = new Vector3(40, 0.5f, 40);
    public Vector3 playerStart;

    void Start()
    {
        //Instantiates the predefined platform prefab at the (0,0,0) position:
        GameObject pf = Instantiate(platform, new Vector3(0,0,0), Quaternion.identity);
        //sets the prefab of the platform to a desired size:
        pf.transform.localScale = platformSize;
        SpawnPlayer();
        SpawnFences();
    }

    void SpawnPlayer()
    {   
        GameObject playr = Instantiate(player, playerStart, Quaternion.identity);
        GameObject camera = Instantiate(cam, new Vector3(playerStart.x, playerStart.y+1f, playerStart.z-5f), Quaternion.identity);
        camera.transform.SetParent(playr.transform);
    }
    void SpawnFences()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(fence, new Vector3(0, 0.5f, i), Quaternion.identity);
            Instantiate(pole, new Vector3(0, 0.5f, i + 0.5f), Quaternion.identity);
        }
    }
}
