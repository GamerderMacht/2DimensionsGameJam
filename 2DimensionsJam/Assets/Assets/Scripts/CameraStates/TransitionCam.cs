using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TransitionCam : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayerClicksOnRobot(GameObject gameObject, Vector3 camPos){
        transform.position = camPos;
        Debug.Log("Cam method has been called");

       
    }
}
