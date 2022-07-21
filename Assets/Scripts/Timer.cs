using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteTheQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool isAnsweringTheQuestion = false;

    public bool loadNextQuestion;
    float timeValue;
    public float fillFraction;
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timeValue = 0;
    }
   public void UpdateTimer()
    {
        timeValue -= Time.deltaTime;

        if(isAnsweringTheQuestion)
        {
            if(timeValue > 0 )
            {
                fillFraction = timeValue / timeToCompleteTheQuestion;
            }
            else
            {
                isAnsweringTheQuestion = false;
                timeValue = timeToShowCorrectAnswer;
            }
        }    
        else
        {
            if (timeValue > 0)
            {
                fillFraction = timeValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringTheQuestion = true;
                timeValue = timeToCompleteTheQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
