using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreHolder;

    [SerializeField] GameObject WinScreenpopup;
    [SerializeField] GameObject DisableQuestionSection;
    quiz quizScript;

    // Start is called before the first frame update
    void Start()
    {
        quizScript = FindObjectOfType<quiz>();
        WinScreenpopup.SetActive(false);
        DisableQuestionSection.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(quizScript.isCompleted && this != null)
        {
            WinScreenpopup.SetActive(true);
            DisableQuestionSection.SetActive(false);
            ScoreHolder.text = quizScript.scoreText.text;
        }
    }

    public void LoadLevel(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
