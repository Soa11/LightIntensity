using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightEnum : MonoBehaviour
{

    public Light lightToFade;
    public float eachFadeTime = 2f;
    public float fadeWaitTime = 5f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeInAndOut());
    }

    private IEnumerator fadeInAndOut()
    {
        throw new NotImplementedException();
    }

    private IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration)
    {
        float minLuminosity = 0; // min intensity
        float maxLuminosity = 10; // max intensity

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        }
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return null;
   
        }

       
        }





    }
















   
