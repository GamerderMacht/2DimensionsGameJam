using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hacking_Manager : MonoBehaviour
{
    public List<HackingPromptSO> hackingPrompts;
    public TextMeshProUGUI hackingPromptText;
    public Button[] buttonAnswers;
    public Slider slider;
    HackingPromptSO selectedPrompt;
    int answerIndex;
<<<<<<< HEAD:2DimensionsJam/Assets/Assets/Scripts/Hacking_Manager.cs
    public bool isAnswerCorrect {get; private set;}
=======
    public int nextQuestionDelay = 3;
    int gotCorrect;
    int gotIncorrect;
>>>>>>> Andrew:2DimensionsJam/Assets/Scripts/Hacking_Manager.cs

    void Start()
    {
        HackingMiniGame();
    }
    void Update() 
    {
        
    }

    public void HackingMiniGame()
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
        }
    }

    public void OnOptionButtonClicked(int answerIndex)
    {
<<<<<<< HEAD:2DimensionsJam/Assets/Assets/Scripts/Hacking_Manager.cs
        //Debug.Log(answerIndex);
        if (answerIndex == selectedPrompt.GetCorrectAnswerIndex())
        {
            //Debug.Log("Correct");
            hackingPromptText.text = "Correct";
            IsoCamera.SwitchPerspectives();
            StartCoroutine(HideHackingUI());
        }
        else if (answerIndex != selectedPrompt.correctAnswerIndex)
        {
            //Debug.Log("Incorrect");
            hackingPromptText.text = "Incorrect";
            isAnswerCorrect = false;
=======

        //Debug.Log(answerIndex);
        if (answerIndex == selectedPrompt.GetCorrectAnswerIndex())
        {
            hackingPromptText.text = "Correct";
            gotCorrect += 1;
            ProgressBar();
            Debug.Log(gotCorrect + "Correct");
        }
        else if (answerIndex != selectedPrompt.correctAnswerIndex)
        {
            hackingPromptText.text = "Incorrect";
            gotIncorrect += 1;
            Debug.Log(gotIncorrect + "Incorrect");
        }

        Invoke("HackingMiniGame", nextQuestionDelay);

    }

    public void ProgressBar()
    {
        slider.value = gotCorrect;
        if (slider.value == slider.maxValue)
        {
            slider.value = 0;
            gotCorrect = 0;
            //Insert Function() that transitions player from hacking to robot
        }
    }
    public void DisableButtonsForSeconds()
    {
        foreach (var button in buttonAnswers)
        {
            button.interactable = false;
        }
        Invoke("EnableButtons", nextQuestionDelay);
    }

    public void EnableButtons()
    {
        foreach (var button in buttonAnswers)
        {
            button.interactable = true;
>>>>>>> Andrew:2DimensionsJam/Assets/Scripts/Hacking_Manager.cs
        }
    }

    private IEnumerator HideHackingUI(){
        yield return new WaitForSeconds(2f);
        GameObject.Find("Hacking Canvas").SetActive(false);
    }

}
