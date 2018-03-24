using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EventType
{
    Fire, Water, Earth, Air, Book, None
}


public class WorldEvents : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_interactables = new List<GameObject>();
    [SerializeField]
    private GameObject m_water, m_fire, m_air, m_earth, m_book;
    [SerializeField]
    private GameObject m_controllerLeft;
    [SerializeField]
    private float m_crossFadeTime = 2f;


    private EventType m_currEvent = EventType.None;



    private void OnEnable()
    {
        ViveInput.interact += Interaction;
        SpeakScript.endMonologue += EnableIcons;
    }


    private void OnDisable()
    {
        ViveInput.interact       -= Interaction;
        SpeakScript.endMonologue -= EnableIcons;
    }



    private void EnableIcons()
    {
        foreach(var interactable in m_interactables)
        {
            if(interactable.name == EventType.Book.ToString() + " Interactable")
                continue;

            interactable.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        }
    }


    private void Interaction(EventType eventType)
    {
        bool m_fadedOut = false;

        foreach(var interactable in m_interactables)
        {
            if(m_currEvent == eventType) continue;

            if(interactable.name == eventType.ToString() + " Interactable")
            {
                interactable.SetActive(true);
                if(interactable.name == "Person Interactable")
                {
                    StartCoroutine(FadeAudio.FadeIn(interactable.GetComponent<SpeakScript>().m_audioSource));
                }
                else
                {
                    StartCoroutine(FadeAudio.FadeIn(interactable.GetComponent<AudioSource>()));
                }
                
                
                //var fadeOutInter = 
                //
                //StartCoroutine(FadeAudio.FadeOut(interactable.GetComponent<AudioSource>()));
            }
            else
            {
                
                interactable.SetActive(false);
            }
        }
        m_currEvent = eventType;








        //switch(eventType)
        //{
        //    case EventType.Fire:
        //        m_fire.SetActive(true);
                
        //        m_water.SetActive(false);
        //        m_air.SetActive(false);
        //        m_earth.SetActive(false);
               
        //        break;
        //    case EventType.Water:
        //        m_water.SetActive(true);

        //        m_fire.SetActive(false);
        //        m_air.SetActive(false);
        //        m_earth.SetActive(false);
        //        break;

        //    case EventType.Earth:
        //        m_earth.SetActive(true);

        //        m_water.SetActive(false);
        //        m_fire.SetActive(false);
        //        m_air.SetActive(false);
        //        break;

        //    case EventType.Air:
        //        m_air.SetActive(true);

        //        m_water.SetActive(false);
        //        m_fire.SetActive(false);
        //        m_earth.SetActive(false);

        //        break;


        //    case EventType.Book:
        //        m_book.SetActive(true);

        //        break;
        //    case EventType.None:
        //        break;
        //    default:
        //        break;
        //}
    }

    private void FadeOutEffect(EventType nextType)
    {
        switch(nextType)
        {
            case EventType.Fire:
//                m_fire.GetComponent<Fire>().m_Brightness;

                break;
            case EventType.Water:
                break;
            case EventType.Earth:
                break;
            case EventType.Air:
                break;
            case EventType.Book:
                break;
            case EventType.None:
                break;
            default:
                break;
        }
    }


    private void CrossFadeEffects(EventType nextType)
    {

    }




    private IEnumerator FadeOut()
    {
        yield return false;
    }
}
