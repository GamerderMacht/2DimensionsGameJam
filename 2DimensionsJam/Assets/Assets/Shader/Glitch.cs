using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Glitch : MonoBehaviour
{
    public Material glitchMaterial;
    public float intensity = 0.1f;

    private Renderer rend;
    private float offset1;
    private float offset2;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        offset1 = Random.Range(0f, 1f);
        offset2 = Random.Range(0f, 1f);
    }

    private void Update()
    {
        offset1 += Time.deltaTime * intensity;
        offset2 += Time.deltaTime * intensity;

        glitchMaterial.SetVector("_GlitchOffset", new Vector4(offset1, offset2, 0f, 0f));
        rend.material = glitchMaterial;
    }

}
