using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

enum IntensityDirection
{
    Increase,
    Same,
    Decrease
}

public class scanVal : MonoBehaviour
{

    IntensityDirection myDir = IntensityDirection.Same;

    private int oldVal = 0;

    public UnitySerialPort sp;

    private string str = "";
    public int[] curValues;
    public int[] oldValues;
    private IntensityDirection[] directions;

    // value used for timer check
    private float timer = 0.0f;
    //timer interval
    public float period = 0.1f;


    public float minIntensity = 0f;
    public float maxIntensity = 10f;
    public float lightIntensityMultiplier = 1;
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

        //init values with specific array member
        int o = oldValues[8];
        int c = curValues[8];

        Debug.Log(c);

        if (o < c)
        {
            myDir = IntensityDirection.Increase;
            
        }
        if (o == c)
        {
            myDir = IntensityDirection.Same;
        }
        if (o > c)
        {
            myDir = IntensityDirection.Decrease;
        }

        ChangeLightIntensity(myDir);
        

    }

    void ChangeLightIntensity(IntensityDirection direction)
    {
        //direction is the name of the local direction variable
        float dt = Time.deltaTime;
        //print(direction);

        switch (direction)
        {
            case IntensityDirection.Increase:
                lt.intensity += dt * lightIntensityMultiplier;
                break;
            case IntensityDirection.Same:
                // lt.intensity -= dt * lightIntensityMultiplier;
                break;
            case IntensityDirection.Decrease:
                lt.intensity -= dt * lightIntensityMultiplier;
                break;
        }

        lt.intensity = Mathf.Clamp(lt.intensity, minIntensity, maxIntensity);
        //Debug.Log(lt.intensity);

        oldValues = curValues;

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

}