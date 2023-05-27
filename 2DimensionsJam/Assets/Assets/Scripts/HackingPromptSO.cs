using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hacking Prompt", menuName = "Hacking Prompt")]

public class HackingPromptSO : ScriptableObject
{
    [TextArea(2,6)]
    public string hackingPrompt;
    public string[] answers = new string[4];
    public int correctAnswerIndex;

    public string GetHackingPrompt()
    {
        return hackingPrompt;
    }

    public string GetAnswers(int index)
    {
        return answers[index];
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }


}
