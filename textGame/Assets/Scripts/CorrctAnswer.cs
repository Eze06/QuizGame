using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CorrctAnswer : MonoBehaviour
{

    public quizManagerV2 quizManager;

    public int buttonPressed;

    public GameObject Tick;
    public GameObject Cross;


    private void Start()
    {
        DontDestroyOnLoad(this);
        //quizManager = GetComponent<quizManagerV2>();
    }
    public void BtnPress(GameObject buttonNumber)
    {
        if (buttonNumber.CompareTag("Option 1"))
        {
            buttonPressed = 1;
        }
        else if (buttonNumber.CompareTag("Option 2"))
        {
            buttonPressed = 2;
        }
        else if (buttonNumber.CompareTag("Option 3"))
        {
            buttonPressed = 3;
        }
        else
        {
            buttonPressed = 4;
        }


        StartCoroutine(WaitBeforeShow());



    }

    IEnumerator WaitBeforeShow()
    {
        if (buttonPressed == quizManager.QnA[quizManager.currentQuestion].CorrectAnswers)
        {
            quizManager.IncreaseScore();
            Tick.SetActive(true);

            yield return new WaitForSeconds(0.5f);


            Tick.SetActive(false);
            quizManager.nextQuestion();
        }
        else
        {
            Cross.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            Cross.SetActive(false);
            quizManager.nextQuestion();
        }


    }



}

