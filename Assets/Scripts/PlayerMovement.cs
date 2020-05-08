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
    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        //transform.Translate(0f, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        //if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {

            //if (!(Physics.Raycast(GameObject.Find("Player(Clone)").transform.position, GameObject.Find("Player(Clone)").transform.TransformDirection(Vector3.forward), 0.7f))) 
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(GameObject.Find("Player(Clone)").transform.position, GameObject.Find("Player(Clone)").transform.forward);
            if ( !Physics.Raycast(ray, out hit, 1) || (Physics.Raycast(ray, out hit, 1) && hit.collider.gameObject.name!="Fence(Clone)"))
            {
               MazeGen.solve(MazeGen.mazeRaw,
                    (int)GameObject.Find("Player(Clone)").transform.position.x,
                    (int)GameObject.Find("Player(Clone)").transform.position.z,
                    (int)GameObject.Find("Pole(Clone)").transform.position.x,
                    (int)GameObject.Find("Pole(Clone)").transform.position.z);
                StreamReader read = new StreamReader("Assets/Scripts/Maze.txt");
                ArrayList mazeSolved = new ArrayList();
                
                while (!read.EndOfStream)
                {
                    mazeSolved.Add(read.ReadLine());
                }
                read.Close();
                //StringBuilder str = new StringBuilder(mazeSolved[(int)GameObject.Find("Player(Clone)").transform.position.x].ToString());//Set Current position as .
                //str[(int)GameObject.Find("Player(Clone)").transform.position.z] = ' ';
                //mazeSolved[(int)GameObject.Find("Player(Clone)").transform.position.x] = str;

                StartCoroutine("Forward");

                if (mazeSolved[(int)GameObject.Find("Player(Clone)").transform.position.x].ToString()[(int)GameObject.Find("Player(Clone)").transform.position.z] != '.')
                {
                    using (StreamWriter sw = File.AppendText(Application.dataPath + Spawner.path))
                    {
                        if (AudioCue.play.isPlaying)
                        {
                            //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                            sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Forward" + ",yes" + ","+ AudioCue.currentlyPlaying + "," + Time.time);
                            sw.Close();
                        }
                        else
                        {
                            //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                            sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Forward" + ",yes" + ",0," + Time.time);
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
                            sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Forward" + ",no" + ","+ AudioCue.currentlyPlaying + "," + Time.time);
                            sw.Close();
                        } else
                        {
                            //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                            sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Forward" + ",no" + ",0" + "," + Time.time);
                            sw.Close();
                        }
                    }
                }


            }
            lastTime = Time.time;
        }
        //if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        //{
        //    StartCoroutine("Backward");
        //    lastTime = Time.time;
        //}
        if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            StartCoroutine("Left");
            lastTime = Time.time;
            using (StreamWriter sw = File.AppendText(Application.dataPath + Spawner.path))
            {
                if (AudioCue.play.isPlaying)
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Turns Left" + ",N/A" + ","+ AudioCue.currentlyPlaying + "," + Time.time);
                    sw.Close();
                }
                else
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Turns Left" + ",N/A" + ",0" + "," + Time.time);
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
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Turns Right" + ",N/A" + ","+ AudioCue.currentlyPlaying + "," + Time.time);
                    sw.Close();
                }
                else
                {
                    //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                    sw.WriteLine(MainMenu.ID + "," + MainMenu.trialType + "," + Spawner.attemptNumber + ",Turns Right" + ",N/A" + ",0" + "," + Time.time);
                    sw.Close();
                }
                
            }
        }

    }


    
    public IEnumerator Forward()
    {
        for (int i = 0; i < 25; i++)
        {
            transform.Translate(Vector3.forward * 0.04f);
            yield return null;
        }
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

    void LogMovement(Vector3 position)
    {

    }
    //IEnumerator Backward()
    //{
    //    for (int i = 0; i < 50; i++)
    //    {
    //        transform.Translate(Vector3.forward * -1 * 0.02f);
    //        yield return null;
    //    }
    //}

}