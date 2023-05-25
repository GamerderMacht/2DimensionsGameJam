using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


public class MainMenuScript : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
    public AudioClip hoverSound;
    public AudioSource menuaudioListener;
    // Start is called before the first frame update
    void Start()
    {
        currentCamera.Priority++;
    }

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;

        currentCamera = target;

        currentCamera.Priority++;
    }
    public void MouseHoverSound()
    {
        menuaudioListener.PlayOneShot(hoverSound);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main_Game");
    }

    public void CloseGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
