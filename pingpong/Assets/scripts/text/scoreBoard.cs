using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scoreBoard : MonoBehaviour
{
    public TextMeshProUGUI score;
    public int counterL = -2;
    public int counterR = -2;


    // Start is called before the first frame update
    void Start()
    {
        score.text = "Current score is " + '\n' + "Left: " + counterL + " Right: " + counterR;
    }

    /*public void IncrementCount()
    {
        counter++;
        score.text = "the current score is " + counter;
    }*/

    public void IncrementCountLeft()
    {
        if(counterR < 11)
        {
            counterL++;
            
        }

        showScore();

    }


    public void IncrementCountRight()
    {
        if(counterR < 11)
        {
            counterR++;
            
        }
        showScore();

    }

    public void showScore()
    {
        string first = changeColor().Item1;
        string second = changeColor().Item2;
        string third = changeColor().Item3;
        score.text = $"Current score is \n left:{first}{counterL}    right:{third}{counterR}";


    }

    private (string,string,string) changeColor()
    {
        string ret1 = (counterL > 6) ? "<color=blue>" : "<color=purple>";

        string ret2 = "color";

        string ret3 = (counterR < 7) ? "<color=orange>" : "<color=red>";

        return (ret1, ret2, ret3);
    }

}
