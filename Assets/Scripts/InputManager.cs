using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;

public class InputManager : MonoBehaviour 
{
    private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;

    [SerializeField]
    private SteamVR_TrackedObject m_trackedLeftController;
    private SteamVR_Controller.Device m_leftController{ get { return SteamVR_Controller.Input((int)m_trackedLeftController.index); } }
    
    
    
    
    private void Update()
    {
        if(m_leftController.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("Select");
        }

    }


}
