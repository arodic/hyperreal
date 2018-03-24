using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementWaterController : MonoBehaviour {

  private ElementController controller;
  private rainbowController rainbow;

	void Start () {
    controller = GetComponent<ElementController>();
    rainbow = GetComponent<rainbowController>();
	}

	void Update () {
    if (controller && rainbow) {
      rainbow._Curl = 0.0075f * controller.fade;
    }
	}

}
