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
    private GameObject m_controllerRight;
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
        ViveInput.interact -= Interaction;
        SpeakScript.endMonologue -= EnableIcons;
    }



    private void EnableIcons()
    {
        foreach(var interactable in m_interactables)
        {
            if(interactable.name == EventType.Book.ToString() + " Interactable")
                continue;

            interactable.SetActive(true);
        }
    }


    private void Interaction(EventType eventType)
    {
        switch(eventType)
        {
            case EventType.Fire:
                m_fire.GetComponent<ElementController>().active = true;

                m_water.GetComponent<ElementController>().active = false;
                m_air.GetComponent<ElementController>().active = false;
                m_earth.GetComponent<ElementController>().active = false;


                StartCoroutine(FadeAudio.FadeIn(m_fire.GetComponent<AudioSource>()));

                StartCoroutine(FadeAudio.FadeOut(m_water.GetComponent<AudioSource>()));
                StartCoroutine(FadeAudio.FadeOut(m_air.GetComponent<AudioSource>()));
                StartCoroutine(FadeAudio.FadeOut(m_earth.GetComponent<AudioSource>()));


                break;
            case EventType.Water:
                m_water.GetComponent<ElementController>().active = true;

                m_fire.GetComponent<ElementController>().active = false;
                m_air.GetComponent<ElementController>().active = false;
                m_earth.GetComponent<ElementController>().active = false;





                StartCoroutine(FadeAudio.FadeIn(m_water.GetComponent<AudioSource>()));

                StartCoroutine(FadeAudio.FadeOut(m_fire.GetComponent<AudioSource>()));
                StartCoroutine(FadeAudio.FadeOut(m_air.GetComponent<AudioSource>()));
                StartCoroutine(FadeAudio.FadeOut(m_earth.GetComponent<AudioSource>()));



                break;

            case EventType.Earth:
                m_earth.GetComponent<ElementController>().active = true;

                m_water.GetComponent<ElementController>().active = false;
                m_fire.GetComponent<ElementController>().active = false;
                m_air.GetComponent<ElementController>().active = false;



                StartCoroutine(FadeAudio.FadeIn(m_earth.GetComponent<AudioSource>()));

                StartCoroutine(FadeAudio.FadeOut(m_water.GetComponent<AudioSource>()));
                StartCoroutine(FadeAudio.FadeOut(m_water.GetComponent<AudioSource>()));
                StartCoroutine(FadeAudio.FadeOut(m_air.GetComponent<AudioSource>()));
                break;

            case EventType.Air:
                m_air.GetComponent<ElementController>().active = true;

                m_water.GetComponent<ElementController>().active = false;
                m_fire.GetComponent<ElementController>().active = false;
                m_earth.GetComponent<ElementController>().active = false;




                StartCoroutine(FadeAudio.FadeIn(m_air.GetComponent<AudioSource>()));

                StartCoroutine(FadeAudio.FadeOut(m_water.GetComponent<AudioSource>()));
                StartCoroutine(FadeAudio.FadeOut(m_fire.GetComponent<AudioSource>()));
                StartCoroutine(FadeAudio.FadeOut(m_earth.GetComponent<AudioSource>()));
                break;

            case EventType.Book:
                m_book.SetActive(true);

                break;
            case EventType.None:
                break;
            default:
                break;
        }
    }
}
