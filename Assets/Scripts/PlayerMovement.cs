using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public int moveDist = 1;
    public float rotatespeed = 200f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);
        //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        //{
        //    //print(GameObject.Find("Player(Clone)").transform.position.ToString());
        //    transform.position = Vector3.Lerp(this.transform.position,
        //        new Vector3(0,1,0),
        //        100f);

        //}
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * rotatespeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotatespeed * Time.deltaTime);
        }

    }
}