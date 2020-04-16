using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioCue : MonoBehaviour
{
    void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("AUDIO CUE");
        }
    }
}