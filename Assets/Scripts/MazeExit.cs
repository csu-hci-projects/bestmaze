using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeExit : MonoBehaviour
{
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("YOU WIN");
        }
    }
}