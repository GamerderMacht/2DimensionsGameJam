using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VRAMGlitchEffect : MonoBehaviour
{
    public Material glitchMaterial;  // The material that applies the glitch effect
    public float glitchIntensity = 0.1f;  // The intensity of the glitch effect
    public float glitchInterval = 0.5f;  // The interval between glitch occurrences

    private Camera cam;
    private float glitchTimer = 0f;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        glitchTimer += Time.deltaTime;

        if (glitchTimer >= glitchInterval)
        {
            glitchTimer = 0f;
            StartCoroutine(ApplyGlitch());
        }
    }

    private IEnumerator ApplyGlitch()
    {
        // Set the glitch effect intensity in the material
        glitchMaterial.SetFloat("_Intensity", glitchIntensity);

        // Enable the material on the camera
        cam.SetReplacementShader(glitchMaterial.shader, "");

        // Wait for a short duration to apply the glitch effect
        yield return new WaitForSeconds(0.1f);

        // Disable the glitch effect
        cam.ResetReplacementShader();
    }
}

