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
    public GameObject camera;

    void Start()
    {
        //Instantiates the predefined platform prefab at the (0,0,0) position:
        GameObject pf = Instantiate(platform, new Vector3(0,0,0), Quaternion.identity);
        //sets the prefab of the platform to a desired size:
        pf.transform.localScale = new Vector3(20, 0.5f, 40);

        Instantiate(player, new Vector3(3, 0.5f, 3), Quaternion.identity);
        Instantiate(camera, new Vector3(3, 0.5f, 3), Quaternion.identity);



        for (int i=0; i < 4; i++)
        {
            Instantiate(fence, new Vector3(0, 0.5f, i), Quaternion.identity);
            Instantiate(pole, new Vector3(0, 0.5f, i+0.5f), Quaternion.identity);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
