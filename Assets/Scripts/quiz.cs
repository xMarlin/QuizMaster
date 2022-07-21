using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI Questiontext;

    [Header("Answers")]
    [SerializeField] GameObject[] AnswerButtons;
    int correctAnswerInder;
    bool hasAnswerEarly;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correcttAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        DisplayQuestions();
    }
    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            timer.loadNextQuestion = false;
            GetNextQustion();
        }
    }

    private void DisplayQuestions()
    {
        //this Get Question Text
        Questiontext.text = question.GetQuestion();
        //For loop to Display the answer on ButtonText
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswers(i);
        }

        SetButtonStat(true); // to set button interatable true once the Quesion is loaded
        ResetButtonSpriteToDefault(); // to set button sprite to the default sprite once the question load
    }

    // Selecting the answer from the buttons
    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);
        SetButtonStat(false);
        timer.CancelTimer();
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == question.GetCorrectAnswer())
        {
            Questiontext.text = "Correct!";
            buttonImage = AnswerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correcttAnswerSprite;
        }
        else
        {
            correctAnswerInder = question.GetCorrectAnswer();
            string correctAnswer = question.GetAnswers(correctAnswerInder);
            Questiontext.text = "sorry the correct answer was: " + correctAnswer;
            buttonImage = AnswerButtons[correctAnswerInder].GetComponent<Image>();
            buttonImage.sprite = correcttAnswerSprite;
            hasAnswerEarly = true;

        }
    }

    void SetButtonStat(bool state)
    {
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            Button button = AnswerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void ResetButtonSpriteToDefault()
    {
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            Image buttonImage = AnswerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void GetNextQustion()
    {

    }
}
