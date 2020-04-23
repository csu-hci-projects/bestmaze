using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioCue : MonoBehaviour
{
    private int perspective = MainMenu.POV; //egocentric: 0, allocentric: 1
    static AudioSource play;
    public static AudioClip [] egoSounds;
    public static AudioClip[] alloSounds;

    void Start()
    {
        egoSounds = new AudioClip[60];
        alloSounds = new AudioClip[60];
        for (int i = 0; i < 60; i++)
        {
            egoSounds [i] = Resources.Load<AudioClip>("Ego_" + (i+1));
            alloSounds [i]= Resources.Load<AudioClip>("Allo_" + (i+1));
        }
        Debug.Log(egoSounds);
        play = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (perspective == 0)
            {
                //chose random ego audio cue
                if (!play.isPlaying)
                {
                    play.PlayOneShot(egoSounds[Random.Range(0,egoSounds.Length)]);
                }
            }
            else if(perspective == 1)
            {
                //chose random allo audio cue
                if (!play.isPlaying)
                {
                    play.PlayOneShot(alloSounds[Random.Range(0, egoSounds.Length)]);
                }

            }

            Debug.Log("AUDIO CUE");
        }
    }
}