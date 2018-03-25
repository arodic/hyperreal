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
    private bool waterHit, earthHit, airHit, fireHit = false;




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



    private void Update()
    {
        if(waterHit && earthHit && airHit && fireHit)
        {
            print("All icons hit");
            StartCoroutine(WaitForTime(15f, result =>
            {
                if(result)
                {
                    Debug.Log("GLITCH EFFECT");
                    m_book.GetComponent<ElementController>().active = false;
                }
            }));

        }
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

                fireHit = true;

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

                waterHit = true;

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


                earthHit = true;

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

                airHit = true;
                break;

            case EventType.Book:
                m_book.GetComponent<ElementController>().active = true;


                break;
            case EventType.None:
                break;
            default:
                break;
        }
    }



    private IEnumerator WaitForTime(float seconds, System.Action<bool> flag)
    {
        flag(false);
        yield return new WaitForSeconds(seconds);
        flag(true);
    }
}
