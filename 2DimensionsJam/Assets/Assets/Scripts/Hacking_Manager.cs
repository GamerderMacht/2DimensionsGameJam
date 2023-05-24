using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hacking_Manager : MonoBehaviour
{
    public List<HackingPromptSO> hackingPrompts;
    public TextMeshProUGUI hackingPromptText;
    public GameObject[] buttonAnswers;
    HackingPromptSO selectedPrompt;
    int answerIndex;

    void Start()
    {
        // Select a random hacking prompt from the list
        selectedPrompt = hackingPrompts[Random.Range(0, hackingPrompts.Count)];

        // Assign the hacking prompt text to the UI text component
        hackingPromptText.text = selectedPrompt.hackingPrompt;

        // Assign each option to a button
        for (int i = 0; i < buttonAnswers.Length; i++)
        {
            
            int answerIndex = i;
            buttonAnswers[i].GetComponentInChildren<TextMeshProUGUI>().text = selectedPrompt.answers[i];
            //buttonAnswers[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnOptionButtonClicked(i));
        }
    }

    public void OnOptionButtonClicked(int answerIndex)
    {
        Debug.Log(answerIndex);
        if (answerIndex == selectedPrompt.GetCorrectAnswerIndex())
        {
            Debug.Log("Correct");
            hackingPromptText.text = "Correct";
        }
        else if (answerIndex != selectedPrompt.correctAnswerIndex)
        {
            Debug.Log("Incorrect");
            hackingPromptText.text = "Incorrect";
        }
    }

}
