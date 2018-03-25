using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEarthController : MonoBehaviour
{
    private ElementController controller;
    public RTIvyController ivy;
    public Material mat1;
    public Material mat2;
    public AudioSource audioSource; 

    void Start()
    {
        controller = GetComponent<ElementController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(controller && ivy)
        {
            audioSource.volume = 1;

            if(!controller.active)
            {
                ivy.elapsedTime = 0.0f;
                audioSource.volume = 0f;
            }

            
            ivy.IvyEnabled = controller.active;
            if(mat1)
            {
                mat1.SetColor("_Color", new Color(1f, 1f, 0f, controller.fade));
            }
            if(mat2)
            {
                mat2.SetColor("_Color", new Color(1f, 1f, 0f, Mathf.Pow(controller.fade, 5.0f)));
            }
        }
    }

}
