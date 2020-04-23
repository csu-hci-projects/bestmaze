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
        mazeGen = new MazeGen();

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0f, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            if (!(Physics.Raycast(GameObject.Find("Player(Clone)").transform.position, GameObject.Find("Player(Clone)").transform.TransformDirection(Vector3.forward), 1)))
            {
               mazeGen.solve(MazeGen.mazeRaw,
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
                    using (StreamWriter sw = File.AppendText(Application.dataPath + ("/ParticipantData.csv")))
                    {
                        //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                        sw.WriteLine(MainMenu.ID + ",D" + ",1" + ",Forward"+",yes"+",TBD"+"," + Time.time);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(Application.dataPath + ("/ParticipantData.csv")))
                    {
                        //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                        sw.WriteLine(MainMenu.ID + ",D" + ",1" + ",Forward" + ",no" + ",TBD" + "," + Time.time);
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
            using (StreamWriter sw = File.AppendText(Application.dataPath + ("/ParticipantData.csv")))
            {
                //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                sw.WriteLine(MainMenu.ID + ",D" + ",1" + ",Turns Left" + ",no" + ",TBD" + "," + Time.time);
            }
        }
        if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            StartCoroutine("Right");
            lastTime = Time.time;
            using (StreamWriter sw = File.AppendText(Application.dataPath + ("/ParticipantData.csv")))
            {
                //"ParticipantID,DataType,AttemptNumber,Movement,Error,AudioCue,Time,Gender,VideoGame"
                sw.WriteLine(MainMenu.ID + ",D" + ",1" + ",Turns Right" + ",no" + ",TBD" + "," + Time.time);
            }
        }

    }
    
    IEnumerator Forward()
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