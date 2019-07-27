using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityScript.Lang;

public class BoxScript : MonoBehaviour {

    //============= Gameobject Childs nr in list================
    // nr;  1
    // rezultat;  3
    // coeficient; 2
    // x; 0
    //==========================================================

    int nri = 0;
    public static int[] a;
    int z = 0; // increment for X's array 
    int t = 0; // increment for overwrite with 0 value at empty processes
    int k = 1; // increment for ClearBehind() function
    int step = 0; // increment for # step
    int h = 0; // increment for queue()

    private LogScript log;
    private OutputScript _out;

    void Start()
    {
        log = GetComponent<LogScript>();
        _out = GetComponent<OutputScript>();       
    }

    void queue()
    {
        Text[] p;
        p = InitiateScript.list[0].GetComponentsInChildren<Text>();

        Text txt;
        txt = InitiateScript.list1[0].GetComponentInChildren<Text>();

        string lastelem = (a[h]).ToString();
     
        log.AddEvent("Sending " + lastelem + " to P0" + " with the exit result: " + p[3].text);

        txt.text = txt.text.Remove(txt.text.Length - 2);
    }

    void WriteToFirstBox()
    {
        Text[] first;
        first = InitiateScript.list[0].GetComponentsInChildren<Text>();
        if(z >= a.Length)
        {
            z = 0;
            first[0].text = a[z].ToString();
        }
        else
        {
            first[0].text = a[z].ToString();
        }
        first[3].text = InitiateScript.arr[0].ToString();
    }


    void WriteToNextBox()
    {
        Text[] p;
        Text[] prev;
        int x, coeficient, rezultat;

        for (int i = nri; i >= 1; i--)
        {
            p = InitiateScript.list[i].GetComponentsInChildren<Text>();
            prev = InitiateScript.list[i - 1].GetComponentsInChildren<Text>();
            p[0].text = prev[0].text;


            int.TryParse(prev[0].text, out x);
            int.TryParse(p[2].text, out coeficient);
            int.TryParse(prev[3].text, out rezultat);

            rezultat = rezultat * x + coeficient;
            p[3].text = rezultat.ToString();

            log.AddEvent("Sending " + prev[0].text + " from " + prev[1].text + " to " + p[1].text + " with the exit result: " + rezultat.ToString());

            if (i == InitiateScript.list.Count - 1)
            {
                _out.AddEvent("The output of X = " + p[0].text + " is: " + p[3].text);
            }
        }
    }

    void ClearBehind()
    {
        Text[] p;
        Text[] prev;
        for (int i = nri; i >= k; i--)
        {
            p = InitiateScript.list[i].GetComponentsInChildren<Text>();
            prev = InitiateScript.list[i - 1].GetComponentsInChildren<Text>();
            p[0].text = prev[0].text;

            int x, coeficient, rezultat;
            int.TryParse(prev[0].text, out x);
            int.TryParse(p[2].text, out coeficient);
            int.TryParse(prev[3].text, out rezultat);

            rezultat = rezultat * x + coeficient;
            p[3].text = rezultat.ToString();
            log.AddEvent("Sending " + prev[0].text + " from " + prev[1].text + " to " + p[1].text + " with the exit result: " + rezultat.ToString());

            if(i == InitiateScript.list.Count -1)
            {
                _out.AddEvent("The output of X = " + p[0].text + " is: " + p[3].text);                 
            }
        }

        Text[] first;
        first = InitiateScript.list[t].GetComponentsInChildren<Text>();
        first[0].text = "0";
        first[3].text = "0";
    }

    public void OnMouseUp()
    {
        if (nri == 0)
        {
            log.AddEvent("======================= STEP " + step.ToString() + " ==========================");
            WriteToFirstBox();
            nri += 1;
            z += 1;
            step += 1;
            queue();
            h += 1;
        }
        else if(z >= a.Length)
        {
            if (nri >= InitiateScript.list.Count)
            {
                nri = InitiateScript.list.Count - 1;
            }
            log.AddEvent("======================= STEP " + step.ToString() + " ==========================");
            ClearBehind();
            t += 1;
            k += 1;
            step += 1;
            nri += 1;
        }
        else
        {
            log.AddEvent("======================= STEP " + step.ToString() + " ==========================");
            WriteToNextBox();
            WriteToFirstBox();
            queue();
            nri += 1;
            z += 1;
            step += 1;
            h += 1;
        }
    }
}
