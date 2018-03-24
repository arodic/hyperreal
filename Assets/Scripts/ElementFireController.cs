using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFireController : MonoBehaviour {

  private ElementController controller;
  private LightFlicker light;
  private Fire fire;

	void Start () {
    controller = GetComponent<ElementController>();
    light = GetComponent<LightFlicker>();
    fire = GetComponent<Fire>();
	}

	void Update () {
    if (controller && fire) {
      fire.m_Brightness = 16f * controller.fade;
    }
    if (controller && light) {
      light.m_MultiplierMin = 0.2f * controller.fade;
      light.m_MultiplierMax = 1f * controller.fade;
    }
	}


}
