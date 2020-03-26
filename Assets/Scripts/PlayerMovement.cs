using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    public int moveDist = 1;
    public float rotatespeed = 200f;
    float lastTime;
    // Start is called before the first frame update
    void Start()
    {
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0f, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
                
            if (!(Physics.Raycast(GameObject.Find("Player(Clone)").transform.position, GameObject.Find("Player(Clone)").transform.TransformDirection(Vector3.forward), 1)))
            {
                StartCoroutine("Forward");
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
        }
        if ((Time.time - lastTime > 0.2f) && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            StartCoroutine("Right");
            lastTime = Time.time;
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

    //IEnumerator Backward()
    //{
    //    for (int i = 0; i < 50; i++)
    //    {
    //        transform.Translate(Vector3.forward * -1 * 0.02f);
    //        yield return null;
    //    }
    //}

}