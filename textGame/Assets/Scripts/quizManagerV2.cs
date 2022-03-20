using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class quizManagerV2 : MonoBehaviour
{

    /// <summary>
    /// 
    /// When the program runs:
    ///     Set the question
    ///     
    ///         - Using screenshots of lines of code
    ///         - User has to find out the output of the lines of code
    ///         
    ///     Set the options
    ///         -Look at which question we are at
    ///         -Make a for loop to change the text of each button to the correct options for each question
    ///         -Assign correct answer; -Make a boolean
    ///             - check whether [i + 1] == correctanswer
    ///                 - Changes isCorrect to true
    ///             - Else
    ///                 - Change isCorrect to false
    ///
    /// User chooses option:
    ///     Correct option:
    ///         - Goes to next qn
    ///         - Increase overall score (Per Level)
    ///         - Show a message whether you got the answer correct or not
    /// 
    /// </summary>

    //SCORE SYSTREM

    TMP_Text ScoreText;
    TMP_Text LevelText;
    TMP_Text PercentageText;
    TMP_Text QuitText;
    TMP_Text Question;

    public int TotalScore;
    public int[] score;

    public GameObject textCanvas;

    public int currentLevel = 0;
    int totalLevels = 2;
    float scorePercentage;

    bool isPassed;

    //SCORE SYSTEM

    public List<QuestionsNAnswers> QnA;
    public GameObject[] options;

    public int TotalQuestionIndex;
    public Transform questionImages;

    public int currentQuestion;
    int Index = 0;

    public List<int> TempList = new List<int>();

    public CorrctAnswer corrct;

    void Start()
    {

        Array.Resize<GameObject>(ref options, 4);
        Array.Resize<int>(ref  score, currentLevel + 1);

        for (int i = 0; i < 4; i++)
        {
            options[i] = GameObject.FindGameObjectWithTag("Option " + (i + 1).ToString());
        }

        DontDestroyOnLoad(textCanvas);
        DontDestroyOnLoad(this);

        TotalQuestionIndex = questionImages.childCount;


        //RANDOMIZING LIST

        for (int i = 0; i < TotalQuestionIndex; i++)
        {
            TempList.Add(i);
        }

        TempList = TempList.OrderBy(Randomized => System.Guid.NewGuid()).ToList();
    
        
        SetQuestion();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextQuestion();
        }
    }

    public void nextQuestion()
    {

        questionImages.GetChild(currentQuestion).gameObject.SetActive(false);
        Index++;
        SetQuestion();
         
    }

    void SetOptions()
    {

        for (int i = 0; i < options.Length; i++)
        {
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].Answers[i];

        }

    }

    void SetQuestion()
    {
        if (Index >= TempList.Count)
        {
            
            endLevel();
        }

        else
        {

            currentQuestion = TempList[Index];

            questionImages.GetChild(currentQuestion).gameObject.SetActive(true);

            SetOptions();

        }

    }


    public void endLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        textCanvas.SetActive(true);
        scorePercentage = ((float)score[currentLevel] / (float)TotalQuestionIndex) * 100f;
        Debug.Log(scorePercentage + "%");


        ScoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
        LevelText = GameObject.Find("LevelText").GetComponent<TMP_Text>();
        PercentageText = GameObject.Find("Percentage").GetComponent<TMP_Text>();
        QuitText = GameObject.Find("Quit/RestartText").GetComponent<TMP_Text>();

        LevelText.text = "Level " + (currentLevel + 1).ToString() + " Score";
        ScoreText.text = score[currentLevel].ToString();
        PercentageText.text = scorePercentage.ToString() + "%";

        if (scorePercentage < 50f) //FAILED
        {
            isPassed = false;
            PercentageText.color = Color.red;
            QuitText.text = "Restart";

        }
        else //PASSED
        {
            isPassed = true;
            PercentageText.color = Color.green;
            QuitText.text = "Next Level";
        }

    }

    public void IncreaseScore()
    {
        score[currentLevel]++;
    }

    public void ChangeLevel()
    {

        if (isPassed)
        {
            TotalScore += score[currentLevel];
            currentLevel++;
            textCanvas.SetActive(false);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            

            
            
        }
        else
        {
            textCanvas.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        }
    }



}
