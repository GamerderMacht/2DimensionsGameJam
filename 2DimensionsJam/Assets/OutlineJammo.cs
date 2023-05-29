using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineJammo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OutlineJammoRobot()
    {
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = Color.yellow;
        outline.OutlineWidth = 2f;
    }

    public void OutlineJammoRobotNotAnymore()
    {
        Destroy(gameObject.GetComponent<Outline>());

       
    }
}
