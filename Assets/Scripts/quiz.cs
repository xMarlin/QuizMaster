using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class quiz : MonoBehaviour
{
    [Header("-----------Questions-----------")]
    [SerializeField] QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions;
    [SerializeField] TextMeshProUGUI Questiontext;

    [Header("----------Answers-----------")]
    [SerializeField] GameObject[] AnswerButtons;
    int correctAnswerInder;
    bool hasAnswerEarly;

    [Header("----------Buttons Colors-----------")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correcttAnswerSprite;

    [Header("----------Timer------------")]
    [SerializeField] Image timerImage;
    Timer timer;


    [Header("----------Scoring---------")]
    public TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider Progressbar;
    [SerializeField] public bool isCompleted;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        Progressbar.value = 0;
        Progressbar.maxValue = questions.Count;
    }
    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnswerEarly = false;
            timer.loadNextQuestion = false;
            GetNextQustion();
        }
        else if (!hasAnswerEarly && !timer.isAnsweringTheQuestion)
        {
            DisplayAnswer(-1);
            SetButtonStat(false);
        }
    }

    private void DisplayQuestions()
    {
        //this Get Question Text
        Questiontext.text = currentQuestion.GetQuestion();
        //For loop to Display the answer on ButtonText
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswers(i);
        }

        SetButtonStat(true); // to set button interatable true once the Quesion is loaded
        ResetButtonSpriteToDefault(); // to set button sprite to the default sprite once the question load
    }

    // Selecting the answer from the buttons
    public void OnAnswerSelected(int index)
    {
        hasAnswerEarly = true;
        DisplayAnswer(index);
        SetButtonStat(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    private void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswer())
        {
            Questiontext.text = "Correct!";
            buttonImage = AnswerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correcttAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerInder = currentQuestion.GetCorrectAnswer();
            string correctAnswer = currentQuestion.GetAnswers(correctAnswerInder);
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
        if (questions.Count == 0)
            return;
        SetButtonStat(true);
        ResetButtonSpriteToDefault();
        GetRandomQuestion();
        DisplayQuestions();
        scoreKeeper.IncrementQuestionSeen();
        Progressbar.value++;
        if(Progressbar.value == Progressbar.maxValue)
        {
            isCompleted = true;
        }    
    }

    private void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }    
    }
}
