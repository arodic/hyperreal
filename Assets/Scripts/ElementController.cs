using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    public bool active = false;
    public float fadeStep = 0.02f;
    public float fade = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            fade = Mathf.Min(fade + fadeStep * Time.deltaTime, 1.0f);
        }
        else
        {
            fade = Mathf.Max(fade - fadeStep * Time.deltaTime, 0.0f);
        }
    }
}
