using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.VersionControl;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Rigidbody rb;
    //private float amplify = 30f;
    float random;
    float rightPlayer;
    float leftPlayer;
    private AudioSource audioSource;
    public AudioClip clip;
    private AudioSource AudioSource2;
    public AudioClip clip2;
    public scoreBoard scoreBoard;
    int ballB;
    


    // Start is called before the first frame update
    void Start()
    {
        rightPlayer = 0;
        leftPlayer = 0;
        ballB = 0;
        random = Random.Range(0, 2);
        rb = GetComponent<Rigidbody>();

        audioSource = GetComponent<AudioSource>();
        AudioSource2 = GetComponent<AudioSource>();

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



    void OnCollisionEnter(Collision other)
    {
        //changes color of ball every time it hits a paddle
        gameObject.GetComponent<MeshRenderer>().material.color = NewColor();
        Debug.Log("hit");
        

        //going the other way when hitting a paddel
        if (other.gameObject.name == "PaddleLeft")
        {
            rb.AddForce(new Vector3((float).2, 0, (float).5), ForceMode.Impulse);
            other.gameObject.GetComponent<MeshRenderer>().material.color = NewColor();

            if(rb.velocity.magnitude > 25)
            {
                audioSource.PlayOneShot(clip2);
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }


        }

        if (other.gameObject.name == "PaddleRight")
        {
            rb.AddForce(new Vector3((float)-.2, 0, (float).5), ForceMode.Impulse);
            other.gameObject.GetComponent<MeshRenderer>().material.color = NewColor();
            
            if (rb.velocity.magnitude > 25)
            {
                audioSource.PlayOneShot(clip2);
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }


        }

    }

    private Color NewColor()
    {
        Color color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        return color;
    }

    public void incL()
    {
        scoreBoard.IncrementCountRight();
    }


    public void incR()
    {
        scoreBoard.IncrementCountLeft();
        
    }

    public void stopBall()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }

    //changes size of ball whenever it hits the power up cube
    public void biggerBall()
    {
        int ran;
        ran = Random.Range(1, 5);
        rb.transform.localScale = new Vector3(ran, ran, ran);
    }

    //based on where the cube is it will invert the coordinates of x and z and teleport
    public void invertBall()
    {
        int ran;
        ran = Random.Range(-5, 5);
        rb.transform.position = new Vector3(ran, 0, -ran);
    }

    //based on where the cube is it will invert the coordinates of x and z and teleport
    public void invertBall2()
    {
        int ran;
        ran = Random.Range(-5, 5);
        rb.transform.position = new Vector3(-ran, 0, ran);
    }

    void OnTriggerEnter(Collider other)
    {

        //looking for the power up options
        if(this.gameObject.tag == "cube1" || this.gameObject.tag == "cube2" || this.gameObject.tag == "cube3")
        {
            GameObject.Find("Ball").GetComponent<ball>().biggerBall();
        }


        if (this.gameObject.tag == "cube2" || this.gameObject.tag == "cube3")
        {
            GameObject.Find("Ball").GetComponent<ball>().invertBall();
        }

        if (this.gameObject.tag == "cube3")
        {
            GameObject.Find("Ball").GetComponent<ball>().invertBall2();
        }

        //ball hits paddle and creates events
        if (this.gameObject.tag == "leftGoal")
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


            GameObject.Find("Ball").GetComponent<ball>().incL();
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

            GameObject.Find("Ball").GetComponent<ball>().incR();

        }


        //when one of the players reaches 11 points
        if (this.gameObject.tag == "rightGoal" || this.gameObject.tag == "leftGoal")
        {
            int left = scoreBoard.counterL;
            int right = scoreBoard.counterR;

            if (left == 11)
            {
                Debug.Log("Left player has won, congratulations!!");
                leftPlayer = 0;
                Debug.Log("The score is now LeftPaddle: " + leftPlayer + " and RightPaddle: " + rightPlayer);
                GameObject.Find("Ball").GetComponent<ball>().stopBall();
            }
            else if (right == 11)
            {
                Debug.Log("Right player has won, congratulations!!");
                rightPlayer = 0;
                Debug.Log("The score is now LeftPaddle: " + leftPlayer + " and RightPaddle: " + rightPlayer);
                GameObject.Find("Ball").GetComponent<ball>().stopBall();
            }

            GameObject.Find("Ball").GetComponent<ball>().Reset();
        }
    }

}
