using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddle : MonoBehaviour
{
     float amplify = 10;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //by adding a tag to each object I was able to control each paddle seperately in one class like so.
        if(this.gameObject.tag == "PaddleRight")
        {
            if(Mathf.Abs(transform.position.z) < 5.5)
            {
              transform.Translate(new Vector3(0, 0, Input.GetAxis("RightPaddle")) * Time.deltaTime * amplify);
            }
        }

        if (this.gameObject.tag == "PaddleLeft")
        {
            if (Mathf.Abs(transform.position.z) < 5.5)
            {
                transform.Translate(new Vector3(0, 0, Input.GetAxis("LeftPaddle")) * Time.deltaTime * amplify);
            }
        }

    }
}
