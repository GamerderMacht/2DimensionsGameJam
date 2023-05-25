using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup tutoricalCanvas;

    [SerializeField] CanvasGroup pausedCanvas;
    private void Start() {
        
    }
    public AudioClip hoverSound;
    public AudioSource menuaudioListener;
    private void Update() {
        if(Input.GetKey(KeyCode.Escape))
        {
            GamePaused();
        }
    }
    void GamePaused()
    {
        pausedCanvas.alpha = 1f;
        pausedCanvas.interactable = true;
        pausedCanvas.blocksRaycasts = true;
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        pausedCanvas.alpha = 0f;
        pausedCanvas.interactable = false;
        pausedCanvas.blocksRaycasts = false;
        tutoricalCanvas.alpha = 0f;
        Time.timeScale = 1;

    }

    public void TutorialInfo()
    {
        if(tutoricalCanvas.alpha < 0.5f)
        {
            tutoricalCanvas.alpha = 1f;
        }
        else
        {
            tutoricalCanvas.alpha = 0f;
        }
        
    }

    public void QuitPausedGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
        
    }

    public void PausedMouseHoverSound()
    {
        menuaudioListener.PlayOneShot(hoverSound);
    }
}
