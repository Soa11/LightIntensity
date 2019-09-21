using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class scanVal : MonoBehaviour
{
    private int oldVal = 0;

    public UnitySerialPort sp;

    private string str = "";
    public int[] curValues;
    public int[] oldValues;
    //public float oldValueTimeStamp = 0;

    // value used for timer check
    private float timer = 0.0f;
    //timer interval
    public float period = 0.1f;


    public float minLight = 0f;
    public float maxLight = 10f;
    static float t = 0.0f;
    Light lt;
   
    // Use this for initialization
    void Start()
    {
        //init arrays
        curValues = new int[16];  // numbers is a 16-element array
        oldValues = curValues;

        //init port
        sp = UnitySerialPort.Instance;
        sp.OpenSerialPort();


        lt = GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        //catch if SP is closed, make sure it's open
        if (!sp.SerialPort.IsOpen)
        {
            sp.OpenSerialPort();
        }

        str = sp.RawData;
        curValues = splitString(str);

        /*string s = "";

         foreach (int item in curValues)
         {
             s += item.ToString();
             s += ", ";
         }


         print("FIRST: " + s);*/

        //init values with specific array member
        int o = 0;
        int c = 0;

        o = oldValues[3];
        c = curValues[3];
 
        //Timer
        if (Time.time >= timer)
        {
            if (o < c)
            {
                lightOn();

                print("INCREASE");
                oldValues = curValues;
            }
            if (o == c)
            {
                //lt.intensity = lt.intensity;
                lightOn();

                print("same");
                oldValues = curValues;
            }
            if (o > c )
            {
                lt.intensity = Mathf.Lerp(maxLight, minLight, t);
                t += Time.deltaTime * 5 ;

                print("DECREASE");
                oldValues = curValues;
            }

            timer += period;
        }
       
    }

    private int[] splitString(string str)
    {
        int i = 0;
        int[] readings = new int[16];
        int num = 0;

        foreach (string s in str.Split(','))
        {
            if (Int32.TryParse(s, out num))
            {
                if (i <= 15)
                    readings[i++] = num;
            }
        }

        return readings;
    }

    void lightOn()
    {
        lt.intensity = Mathf.Lerp(minLight, maxLight, t);
        t += Time.deltaTime;
    }
  
}