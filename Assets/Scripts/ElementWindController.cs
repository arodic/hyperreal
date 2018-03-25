using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementWindController : MonoBehaviour
{

    private ElementController controller;
    private ParticleSystem particles;

    void Start()
    {
        controller = GetComponent<ElementController>();
        particles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if(controller && particles)
        {
            var emission = particles.emission;
            emission.rateOverTime = 1000f * controller.fade;
        }
    }

}
