using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    Hacking_Manager hackingManager;
    public TMP_Text timerText;
    public float timerCount = 10;
    public bool answeringPhase;
    public bool timeRanOut;

    void Start() 
    {
        hackingManager = FindAnyObjectByType<Hacking_Manager>();    
    }
    void Update()
    {
        TimerCountdown();
    }
    void TimerCountdown()
    {
        if (answeringPhase)
        {
            timerCount -= Time.deltaTime;

            if (timerCount < 0)
            {
                timerCount = 0;
                hackingManager.DisableButtonsForSeconds();
                hackingManager.AnswerWrong();
            }

            int remainingTime = Mathf.FloorToInt(timerCount);

            timerText.text = remainingTime.ToString();
        }
    }
}

