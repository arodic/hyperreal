using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakScript : MonoBehaviour
{

    public delegate void EndMonologue();
    public static event EndMonologue endMonologue;

    [SerializeField]
    public AudioSource m_audioSource;


    private void Start()
    {
        m_audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if(!m_audioSource.isPlaying)
            if(endMonologue != null) endMonologue();
    }
}
