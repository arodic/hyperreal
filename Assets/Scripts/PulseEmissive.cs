using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseEmissive : MonoBehaviour {

  private Material material;
  private float intensity = 0f;

	void Start () {
    material = GetComponent<Renderer>().material;
	}

	void Update () {
    if (material) {
      intensity = Mathf.Sin(Time.time * 2.5f) * 0.5f + 0.5f;
      material.SetColor("_EmissionColor", new Color(intensity, intensity, intensity, intensity));
    }
	}

}
