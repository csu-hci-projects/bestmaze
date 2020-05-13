using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    public int moveDist = 1;
    public float rotatespeed = 200f;
    float lastTime;
    public MazeGen mazeGen;
    public bool finished = false;
    public bool moved = false;
    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
        MazeGen.solve(MazeGen.mazeRaw,
                     (int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x),
                     (int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.z),
                     (int)Mathf.Round(GameObject.Find("Pole(Clone)").transform.position.x),
                     (int)Mathf.Round(GameObject.Find("Pole(Clone)").transform.position.z));
    }


    void check()
    {
        


        //StringBuilder str2 = new StringBuilder(MazeGen.mazeRaw[(int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x)].ToString());//Set Current position as ' '
        //str2[(int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.z)] = ' ';
        //MazeGen.mazeRaw[(int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x)] = str2;

        Spawner.currentAudioType = Spawner.audioTypes[AudioCue.perspective];

            

        //Debug.Log("x: " + (int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x) + ", z: " + (int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.z));

        if (MazeGen.mazeRaw[(int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x)].ToString()[(int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.z)] == ' ')
        {
            using (StreamWriter sw = File.AppendText(Application.dataPath + Spawner.path))
            {
                if (AudioCue.play.isPlaying)
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Forward" + ",yes" + "," + AudioCue.currentlyPlaying + "," + Time.time + "," + Spawner.currentAudioType);
                    sw.Close();
                }
                else
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Forward" + ",yes" + ",0," + Time.time + "," + Spawner.currentAudioType);
                    sw.Close();
                }

            }
        }
        else
        {
            using (StreamWriter sw = File.AppendText(Application.dataPath + Spawner.path))
            {
                if (AudioCue.play.isPlaying)
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Forward" + ",no" + "," + AudioCue.currentlyPlaying + "," + Time.time + "," + Spawner.currentAudioType);
                    sw.Close();
                }
                else
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Forward" + ",no" + ",0" + "," + Time.time + "," + Spawner.currentAudioType);
                    sw.Close();
                }
            }
        }

        for (int i = 0; i < Spawner.mazeCopy.Count; i++)
        {
            MazeGen.mazeRaw[i] = Spawner.mazeCopy[i].ToString();
        }       
    }

    // Update is called once per frame
    void Update()
    {


        //for (int i = 0; i < MazeGen.mazeRaw.Count; i++)
        //{
        //    Debug.Log(MazeGen.mazeRaw[i]);
        //}

        //Debug.Log("x: " + (int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x) + ", z: " + (int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.z));
        if ((Time.time - lastTime > 0.2f) && ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))))
        {           
            //if (!(Physics.Raycast(GameObject.Find("Player(Clone)").transform.position, GameObject.Find("Player(Clone)").transform.TransformDirection(Vector3.forward), 0.7f))) 
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(GameObject.Find("Player(Clone)").transform.position, GameObject.Find("Player(Clone)").transform.forward);
            if ( !Physics.Raycast(ray, out hit, 1) || (Physics.Raycast(ray, out hit, 1) && hit.collider.gameObject.name!="Fence(Clone)"))
            {

                MazeGen.solve(MazeGen.mazeRaw,
                        (int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x),
                        (int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.z),
                        (int)Mathf.Round(GameObject.Find("Pole(Clone)").transform.position.x),
                        (int)Mathf.Round(GameObject.Find("Pole(Clone)").transform.position.z));

                StringBuilder str = new StringBuilder(MazeGen.mazeRaw[(int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x)].ToString());//Set Current position as ' '
                str[(int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.z)] = ' ';
                MazeGen.mazeRaw[(int)Mathf.Round(GameObject.Find("Player(Clone)").transform.position.x)] = str;

                //StartCoroutine("Forward");
                StartCoroutine(SmoothLerp(0.2f));
            }

            lastTime = Time.time;
            
        }

        if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            StartCoroutine("Left");
            lastTime = Time.time;
            using (StreamWriter sw = File.AppendText(Application.dataPath + Spawner.path))
            {
                if (AudioCue.play.isPlaying)
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Turns Left" + ",N/A" + ","+ AudioCue.currentlyPlaying + "," + Time.time + "," + Spawner.currentAudioType);
                    sw.Close();
                }
                else
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Turns Left" + ",N/A" + ",0" + "," + Time.time + "," + Spawner.currentAudioType);
                    sw.Close();
                }
            }
        }
        if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            StartCoroutine("Right");
            lastTime = Time.time;
            using (StreamWriter sw = File.AppendText(Application.dataPath + Spawner.path))
            {
                if (AudioCue.play.isPlaying)
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Turns Right" + ",N/A" + ","+ AudioCue.currentlyPlaying + "," + Time.time + "," + Spawner.currentAudioType);
                    sw.Close();
                }
                else
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Turns Right" + ",N/A" + ",0" + "," + Time.time + "," + Spawner.currentAudioType);
                    sw.Close();
                }
                
            }
        }

    }



    //public IEnumerator Forward()
    //{
    //    moved = true;
    //    for (int i = 0; i < 25; i++)
    //    {
    //        transform.Translate(Vector3.forward * 0.04f);
    //        yield return null;
    //    }
    //    check();
    //}

    public IEnumerator SmoothLerp(float t)
    {

        moved = true;
        //for (int i = 0; i < 25; i++)
        //{
        //    transform.Translate(Vector3.forward * 0.04f);
        //    yield return null;
        //}
        Vector3 startingPos = transform.position;
        Vector3 finalPos = transform.position + transform.forward;
        float elapsed = 0;
        while(elapsed < t)
        {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsed / t));
            elapsed += Time.deltaTime;
            yield return null;
        }
        check();
    }

    IEnumerator Left()
    {
        for (int i = 0; i < 25; i++)
        {
            transform.Rotate(-Vector3.up * 90 * 0.04f);
            yield return null;
        }
    }

    IEnumerator Right()
    {
        for (int i = 0; i < 25; i++)
        {
            transform.Rotate(Vector3.up * 90 * 0.04f);
            yield return null;
        }
    }

}