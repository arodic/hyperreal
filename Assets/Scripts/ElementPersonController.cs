using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPersonController : MonoBehaviour
{
    public ElementController controller;
    public AudioSource glitchAudioSource;
    public AudioSource monologueAudioSource;
    public Material girlMat;

    



    void Update()
    {
        if(controller.active) monologueAudioSource.enabled = true;
    }

}
