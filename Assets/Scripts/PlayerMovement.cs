using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 20f;
    public int moveDist = 1;
    public float rotatespeed = 200f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0f, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartCoroutine("Forward");
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine("Backward");
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine("Left");
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine("Right");
        }
        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        //{
        //    //print(GameObject.Find("Player(Clone)").transform.position.ToString());
        //    transform.position = Vector3.Lerp(this.transform.position,
        //        new Vector3(0,1,0),
        //        100f);

        //}

        //if (input.getkey(keycode.leftarrow) || input.getkey(keycode.a))
        //{
        //    transform.rotate(-vector3.up * rotatespeed * time.deltatime);
        //}
        //else if (input.getkey(keycode.rightarrow) || input.getkey(keycode.d))
        //{
        //    transform.rotate(vector3.up * rotatespeed * time.deltatime);
        //}

    }
    
    IEnumerator Forward()
    {
        for (int i = 0; i < 50; i++)
        {
            transform.Translate(Vector3.forward * 0.02f);
            yield return null;
        }
    }

    IEnumerator Left()
    {
        for (int i = 0; i < 50; i++)
        {
            transform.Rotate(-Vector3.up * 90 * 0.02f);
            yield return null;
        }
    }

    IEnumerator Right()
    {
        for (int i = 0; i < 50; i++)
        {
            transform.Rotate(Vector3.up * 90 * 0.02f);
            yield return null;
        }
    }

    IEnumerator Backward()
    {
        for (int i = 0; i < 50; i++)
        {
            transform.Translate(Vector3.forward * -1 * 0.02f);
            yield return null;
        }
    }
}