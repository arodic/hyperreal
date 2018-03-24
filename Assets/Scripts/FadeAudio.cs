using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAudio : ScriptableObject
{

    public static IEnumerator FadeIn(AudioSource fadeIn)
    {
        fadeIn.volume = 0.0f;
        fadeIn.Play();

        float inTime = 0f;
        while(inTime < 1f)
        {
            inTime += Time.deltaTime;
            fadeIn.volume = inTime;
            yield return new WaitForSeconds(0);
        }
    }


    public static IEnumerator FadeOut(AudioSource fadeOut)
    {
        float outTime = 1f;
        while(outTime > 0f)
        {
            outTime -= Time.deltaTime;
            fadeOut.volume = outTime;
            yield return new WaitForSeconds(0);
        }
        fadeOut.volume = 0.0f;
        fadeOut.Stop();
    }

}
