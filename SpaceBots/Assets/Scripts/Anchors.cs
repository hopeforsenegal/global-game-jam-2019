using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchors : MonoBehaviour
{
    PuzzleControler Pc;

    public int _lockType;

    bool correct;

    private int num;

    public void setCorrect()
    {
        correct = true;
        if (num == 0) {
            Debug.Log("adding to score");
            Pc.score++;
            num = 1;
        }
    }

    public void setIncorrect()
    {
        correct = false;
        if (num == 1)
        {
            Debug.Log("subbing from score");
            Pc.score--;
            num = 0;
        }
    }

    private void Start()
    {
        Pc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PuzzleControler>();
        correct = false;
        num = 0;
    }
}
