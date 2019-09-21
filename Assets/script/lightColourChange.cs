using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightColourChange : MonoBehaviour
{

    public float minLight = 0f;
    
    public float maxLight = 10f;
    static float t = 0.0f;
    Light lt;
    // Interpolate light color between two colors back and forth
    // float duration = 0f;
    //Color color0 = Color.red;
    //Color color1 = Color.blue;

 

    void Start()
    {
        lt = GetComponent<Light>();
    }

    void Update()
    {
        // set light color
        //float t = Mathf.PingPong(Time.time, duration) / duration;
        //print(t);
        //lt.color = Color.Lerp(color0, color1, t);
        
        lt.intensity = Mathf.Lerp(minLight, maxLight, t);
        t += 0.1f * Time.deltaTime;
        



        if (t > 1.0f)
        {
            float temp = maxLight;
            maxLight = minLight;
            minLight = temp;
            t = 0.0f;
        }



    }
   
}
