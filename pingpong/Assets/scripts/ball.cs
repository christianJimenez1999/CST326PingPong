using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Rigidbody rb;
    //private float amplify = 30f;
    float random;
    float rightPlayer;
    float leftPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rightPlayer = 0;
        leftPlayer = 0;
        random = Random.Range(0, 2);
        rb = GetComponent<Rigidbody>();
        //rb.AddForce(new Vector3(4, 0, 4), ForceMode.Impulse);

        //goes to random start to choose a random side to start on
        Invoke("randomStart", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) (  AddForce());
        //rb.AddExplosionForce(this.amplify, Vector3.right * amplify);

        //rb.AddForce(new Vector3(4, 0, 0) * amplify);

    }

    //chooses a random side to start on b/w left and right
    public void randomStart()
    {
        if(random == 0)
        {
            rb.AddForce(new Vector3((float).5 , 0, 1), ForceMode.Impulse);
        }
        else if(random == 1)
        {
            rb.AddForce(new Vector3((float)-.5, 0, 1), ForceMode.Impulse);
        }
    }

    //need to bring ball back to middle of screen after scoring
    public void Reset()
    {
        transform.position = new Vector3(0, 0, 0);
        

    }

    //failed effort
    void OnCollisionEnter(Collision other)
    {
        //changes color of ball every time it hits a paddle
        gameObject.GetComponent<MeshRenderer>().material.color = NewColor();
        Debug.Log("hit");

        //going the other way when hitting a paddel
        if (other.gameObject.name == "PaddleLeft")
        {
            rb.AddForce(new Vector3((float).2, 0, 1), ForceMode.Impulse);
        }

        if (other.gameObject.name == "PaddleRight")
        {
            rb.AddForce(new Vector3((float)-.2, 0, 1), ForceMode.Impulse);
        }

    }

    private Color NewColor()
    {
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        return color;
    }


    void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "leftGoal")
        {

            rightPlayer = rightPlayer + 1;
            if (rightPlayer == 1)
            {
                rb.AddForce(new Vector3((float).5, 0, 1), ForceMode.Impulse);
                Debug.Log("right player scored!!! he now has " + rightPlayer + " goal!! congrats");
                Debug.Log("The score is now LeftPaddle: " + leftPlayer + " and RightPaddle: " + rightPlayer);

            }
            else
            {
                rb.AddForce(new Vector3((float).5, 0, 1), ForceMode.Impulse);
                Debug.Log("right player scored!!! he now has " + rightPlayer + " goals!! congrats");
                Debug.Log("The score is now LeftPaddle: " + leftPlayer + " and RightPaddle: " + rightPlayer);
            }

           
            
        }


        if (this.gameObject.tag == "rightGoal")
        {
            leftPlayer = leftPlayer + 1;
            if (leftPlayer == 1)
            {
                rb.AddForce(new Vector3((float)-.5, 0, 1), ForceMode.Impulse);
                Debug.Log("left player scored!!! he now has " + leftPlayer + " goal!! congrats");
                Debug.Log("The score is now LeftPaddle: " + leftPlayer + " and RightPaddle: " + rightPlayer);
            }
            else
            {
                rb.AddForce(new Vector3((float)-.5, 0, 1), ForceMode.Impulse);
                Debug.Log("left player scored!!! he now has " + leftPlayer + " goals!! congrats");
                Debug.Log("The score is now LeftPaddle: " + leftPlayer + " and RightPaddle: " + rightPlayer);
            }

        }

        if(leftPlayer == 11)
        {
            Debug.Log("Left player has won, congratulations!!");
            leftPlayer = 0;
            Debug.Log("The score is now LeftPaddle: " + leftPlayer + " and RightPaddle: " + rightPlayer);
        } else if(rightPlayer == 11)
        {
            Debug.Log("Right player has won, congratulations!!");
            rightPlayer = 0;
            Debug.Log("The score is now LeftPaddle: " + leftPlayer + " and RightPaddle: " + rightPlayer);
        }

        GameObject.Find("Ball").GetComponent<ball>().Reset();
    }

}
