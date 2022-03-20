using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionsNAnswers
{
    [TextArea(3, 10)]
    public string Question;
    [TextArea(3, 10)]
    public string[] Answers;
    public int CorrectAnswers;

    

}
