using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Hacking_Manager : MonoBehaviour
{
    [SerializeField] GameObject hackingCanvas;
    public List<HackingPromptSO> hackingPrompts;
    public List<HackingPromptSO> selectedPrompts;
    public TextMeshProUGUI hackingPromptText;
    public TextMeshProUGUI amountWrongText;

    public GameObject mainCanvas;

    public Button[] buttonAnswers;
    public Slider slider;
    HackingPromptSO selectedPrompt;
    TimerScript timerScript;
    AudioSource audioSource;
    public AudioClip wrongClip;
    public AudioClip rightClip;
    [SerializeField] GameObject currentRobot;
    int answerIndex;
    public int nextQuestionDelay = 2;
    int gotCorrect;
    int gotIncorrect;
    IsoCamera isocam;

    void Start()
    {
        isocam = GameObject.Find("IsoMetric").GetComponent<IsoCamera>();
        audioSource = this.GetComponent<AudioSource>();
        timerScript = FindAnyObjectByType<TimerScript>();
        
    }
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.L))PlayerLost();
    }

    public void HackingMiniGame()
    {
        timerScript.timeRanOut = false;
        timerScript.timerCount = 11;
        timerScript.answeringPhase = true;
        
        // Select a random hacking prompt from the list
        selectedPrompt = hackingPrompts[Random.Range(0, hackingPrompts.Count)];
        hackingPrompts.Remove(selectedPrompt);
        selectedPrompts.Add(selectedPrompt);

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
        DisableButtonsForSeconds();

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
        
        if (gotIncorrect >= 3)
        {
            amountWrongText.text = "X X X";            
            CloseFailedHacking();
        }
    }
    void PlayerLost()
    {
        
        mainCanvas.GetComponent<Animator>().SetTrigger("PlayerLost");
    }
    public void CloseFailedHacking()
    {
        hackingPromptText.text = "HACK FAILED HACK FAILED HACK FAILED HACK FAILED HACK FAILED";
        audioSource.PlayOneShot(wrongClip);
        CloseEndingHack();
        AddSelectedPromptsBack();
        
        


    }
    public void CloseSuccessedHacking()
    {
        
        //Insert successful hacking (overtaking body)
        CloseEndingHack();
        isocam.SwitchPerspectives();
        //if have already one robot
        Invoke("DelayDestruction", 2f);
    }

    private void DelayDestruction()
    {
        isocam.DestroyOldRobot();
    }
    void CloseEndingHack()
    {
        var hackingCanvasGroup = hackingCanvas.GetComponent<CanvasGroup>();
        hackingCanvasGroup.alpha = 0;
        hackingCanvasGroup.interactable = false;
        hackingCanvasGroup.blocksRaycasts = false;
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
            hackingPromptText.text = "HACK INIZIALIZED! OVERTAKING BODY! HACK INIZIALIZED! OVERTAKING BODY!";
            AddSelectedPromptsBack();
            
            Invoke("CloseSuccessedHacking", 2f);
        }
    }

    public void AddSelectedPromptsBack()
    {
        foreach (HackingPromptSO prompt in selectedPrompts)
        {
            hackingPrompts.Add(prompt);
        }
        selectedPrompts.Clear();
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
