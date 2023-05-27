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
    TimerScript timerScript;
    AudioSource audioSource;
    public AudioClip wrongClip;
    public AudioClip rightClip;
    int answerIndex;
    public int nextQuestionDelay = 3;
    int gotCorrect;
    int gotIncorrect;

    void Start()
    {

        audioSource = this.GetComponent<AudioSource>();
        timerScript = FindAnyObjectByType<TimerScript>();
        HackingMiniGame();
    }
    void Update() 
    {
        
    }

    public void HackingMiniGame()
    {
        timerScript.timeRanOut = false;
        timerScript.timerCount = 11;
        timerScript.answeringPhase = true;
        
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

        if (answerIndex == selectedPrompt.GetCorrectAnswerIndex())
        {
            AnswerCorrect();
        }
        else if (answerIndex != selectedPrompt.correctAnswerIndex)
        {
            AnswerWrong();
        }

    }

    public void AnswerCorrect()
    {
        timerScript.answeringPhase = false;
        hackingPromptText.text = "Correct";
        gotCorrect += 1;
        ProgressBar();
        audioSource.PlayOneShot(rightClip);
        Invoke("HackingMiniGame", nextQuestionDelay);
    }

    public void AnswerWrong()
    {
        timerScript.answeringPhase = false;
        hackingPromptText.text = "Incorrect";
        gotIncorrect += 1;
        Debug.Log(gotIncorrect);
        audioSource.PlayOneShot(wrongClip);
        Invoke("HackingMiniGame", nextQuestionDelay);

        if (gotIncorrect == 1)
        {
            amountWrongText.text = "X";
        }


        if (gotIncorrect == 2)
        {
            amountWrongText.text = "X X";
        }
        
        if (gotIncorrect == 3)
        {
            amountWrongText.text = "X X X";
        }
    }

    public void ProgressBar()
    {
        slider.value = gotCorrect;
        if (slider.value == slider.maxValue)
        {
            amountWrongText.text = "";
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
        }
    }

}
