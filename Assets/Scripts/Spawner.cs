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

    void Start()
    {

        int wallcount = 4;
        Instantiate(platform, new Vector3(0,0,0), Quaternion.identity);
        Instantiate(fence, new Vector3(0,0.5f,0), Quaternion.identity);
        Instantiate(pole, new Vector3(1, 0.5f, 1), Quaternion.identity);
        Instantiate(player, new Vector3(3, 0.5f, 3), Quaternion.identity);
        for (int i=0; i < 4; i++)
        {
            Instantiate(fence, new Vector3(0, 0.5f, i), Quaternion.identity);
            Instantiate(pole, new Vector3(0, 0.5f, i+1), Quaternion.identity);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
