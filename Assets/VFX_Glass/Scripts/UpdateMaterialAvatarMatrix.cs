using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMaterialAvatarMatrix : MonoBehaviour {

  private Matrix4x4 avatar;
  public Material material;
  public ComputeShader computeShader;
  public Transform leftHandCollider;
  public Transform rightHandCollider;
  public Transform headTargetTransform;

  void Awake () {
    avatar = new Matrix4x4();
    if (gameObject.GetComponent<Renderer>() != null) {
      if (gameObject.GetComponent<Renderer>().material) {
        material = gameObject.GetComponent<Renderer>().material;
      }
    }
  }

  void Update () {

    if (leftHandCollider != null) {
      avatar[0, 0] = leftHandCollider.position[0];
      avatar[0, 1] = leftHandCollider.position[1];
      avatar[0, 2] = leftHandCollider.position[2];
    }
    if (rightHandCollider != null) {
      avatar[1, 0] = rightHandCollider.position[0];
      avatar[1, 1] = rightHandCollider.position[1];
      avatar[1, 2] = rightHandCollider.position[2];
    }
    if (headTargetTransform != null) {
      avatar[2, 0] = headTargetTransform.position[0];
      avatar[2, 1] = headTargetTransform.position[1];
      avatar[2, 2] = headTargetTransform.position[2];
    }

    if (material) material.SetMatrix("avatar", avatar);

    if (computeShader) computeShader.SetMatrix("avatar", avatar);

  }

}
