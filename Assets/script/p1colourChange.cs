using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class p1colourChange : MonoBehaviour
{
    //public float r;
    //public float g;
   // public float b;
   // public float a;

    private int oldVal = 0;
    //private double avg;
    //private int sum;

    //public float changingSpeed = 0;
  
    public UnitySerialPort sp;

    private string str = "";
    public int[] curValues; // 
    public int[] oldValues;
    //public float oldValueTimeStamp = 0;
    private float timer = 0.0f;
    public float period = 1f;


    // Use this for initialization
    void Start()
    {
        curValues = new int[16];  // numbers is a 16-element array

        sp = UnitySerialPort.Instance;
        sp.OpenSerialPort();

    }

    // Update is called once per frame
    void Update()
    {
        if (!sp.SerialPort.IsOpen)
        {
            sp.OpenSerialPort();
        }

        str = sp.RawData;
        curValues = splitString(str);




     
        if (Time.time >= timer)
        {
          
            oldValues = curValues;
          
            timer += period;

         

            /*
            if (curValues[0] > oldValues[0])
            {

                Debug.Log("INCREASE");
            }


            if (curValues[0] < oldValues[0])
            {

                Debug.Log("DECREASE");
            }
            */

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
    }
