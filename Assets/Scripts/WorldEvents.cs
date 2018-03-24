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



    private void Update()
    {
        //if(m_water.activeSelf)
        //{
        //    // turn off other objects.
        //    m_fire.SetActive(false);
        //    m_air.SetActive(false);
        //    m_earth.SetActive(false);
        //}
        //else if(m_fire.activeSelf)
        //{
        //    m_water.SetActive(false);
        //    m_air.SetActive(false);
        //    m_earth.SetActive(false);
        //}
        //else if(m_air.activeSelf)
        //{
        //    m_water.SetActive(false);
        //    m_fire.SetActive(false);
        //    m_earth.SetActive(false);
        //}
        //else if(m_earth.activeSelf)
        //{
        //    m_water.SetActive(false);
        //    m_fire.SetActive(false);
        //    m_air.SetActive(false);
        //}
        //else
        //{
        //    // No events active.. Do nothing.
        //}
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
