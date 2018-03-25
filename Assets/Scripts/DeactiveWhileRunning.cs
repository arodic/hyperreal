using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveWhileRunning : MonoBehaviour 
{
    private ElementController m_elementController;



    private void Awake()
    {
        m_elementController = GetComponent<ElementController>();
    }



    private void Update()
    {
        if(m_elementController.active)
        {
            print("Fade OUT");
        }
        else
        {
            Debug.Log("FADE IN");
        }

    }

}
