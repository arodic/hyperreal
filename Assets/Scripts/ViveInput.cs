using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveInput : MonoBehaviour
{
    public delegate void Interact(EventType eventType);
    public static event Interact interact;




    [SerializeField]
    private SteamVR_Controller.Device m_device;
    private SteamVR_TrackedObject m_trackedObject;


    private bool m_select = false;


    private void Start()
    {
        m_trackedObject = GetComponent<SteamVR_TrackedObject>();
    }


    private void Update()
    {
        m_device = SteamVR_Controller.Input((int)m_trackedObject.index);

        if(m_device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("select");
            m_select = true;
        }
        else
        {
            m_select = false;
        }
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.tag != "Interactable") return;

        var type = other.GetComponent<InteractableObject>().Type;
        if(!System.Enum.IsDefined(typeof(EventType), type))
        {
            print("Enum type  " + type.ToString() + " is wrooooonnnngggg!!!");
            return;
        }

        print("Type - " + type);
        if(m_select)
        {
            if(interact != null) interact(type);
        }
    }
}
