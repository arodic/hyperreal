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
    private GameObject m_water, m_fire, m_air, m_earth, m_book;
    [SerializeField]
    private GameObject m_controllerLeft;



    private void OnEnable()
    {
        ViveInput.interact += Interaction;
    }


    private void OnDisable()
    {
        ViveInput.interact -= Interaction;
    }
    


    private void Interaction(EventType eventType)
    {
        switch(eventType)
        {
            case EventType.Fire:
                m_fire.SetActive(true);
                
                m_water.SetActive(false);
                m_air.SetActive(false);
                m_earth.SetActive(false);



                break;
            case EventType.Water:
                m_water.SetActive(true);

                m_fire.SetActive(false);
                m_air.SetActive(false);
                m_earth.SetActive(false);
                break;


            case EventType.Earth:
                m_earth.SetActive(true);

                m_water.SetActive(false);
                m_fire.SetActive(false);
                m_air.SetActive(false);
                break;


            case EventType.Air:
                m_air.SetActive(true);

                m_water.SetActive(false);
                m_fire.SetActive(false);
                m_earth.SetActive(false);

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
