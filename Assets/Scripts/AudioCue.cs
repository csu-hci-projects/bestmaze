﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioCue : MonoBehaviour
{
    public static int perspective = 0; //practice: 0, egocentric: 1, allocentric: 2, none: 3
    public static AudioSource play;
    public static AudioClip egoSound;
    public static AudioClip alloSound;
    public static int chosen;
    public static int currentlyPlaying;

    void Start()
    {
        play = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        chosen = Random.Range(0, 60) + 1;
        while (Spawner.audioCueBank.Contains(chosen))
        {
            chosen = Random.Range(0, 60) + 1;
        }
        Spawner.audioCueBank.Add(chosen);
        currentlyPlaying = chosen;
        egoSound = Resources.Load<AudioClip>("Ego_" + (chosen));
        alloSound = Resources.Load<AudioClip>("Allo_" + (chosen));
        //Debug.Log(chosen);
        if (other.tag == "Player")
        {
            if (perspective == 0 || perspective == 1)
            {
                //chose random ego audio cue
                if (!play.isPlaying)
                {
                    play.PlayOneShot(egoSound);
                }
            }
            else if (perspective == 2)
            {
                //chose random allo audio cue
                if (!play.isPlaying)
                {
                    play.PlayOneShot(alloSound);

                }

            }
        }
    }

}