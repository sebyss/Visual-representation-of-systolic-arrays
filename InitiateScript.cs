using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InitiateScript : MonoBehaviour {

    public GameObject boxf;
    public GameObject box;
    public InputField coef;
    public InputField x;
    public GameObject p;
    public GameObject formula;
   
    public static List<GameObject> list = new List<GameObject>();
    public static List<GameObject> list1 = new List<GameObject>();

    public static int[] arr;

    private LogScript log;
    private OutputScript _out;
    int lengthFormula = 1;

    void Start()
    {
        log = GetComponent<LogScript>();
        _out = GetComponent< OutputScript >();
        log.AddEvent("====================== Progress Log =======================");
        _out.AddEvent("======================= Output Log ========================");
    }

    public void generate()
    {
        

        arr = System.Array.ConvertAll(coef.text.ToString().Split(','), new System.Converter<string, int>(int.Parse));
        BoxScript.a = System.Array.ConvertAll(x.text.ToString().Split(','), new System.Converter<string, int>(int.Parse));

        for (int i = 1; i < arr.Length +1; i++)
        {
            if (i == arr.Length)
            {
                var obsjs = Instantiate(boxf, new Vector2(i * 300, 400), Quaternion.identity, GameObject.FindGameObjectWithTag("Finish").transform);
                list.Add(obsjs);

                log.AddEvent("Created Process : P" + (i - 1).ToString());
            }
            else
            {
                var obsj = Instantiate(box, new Vector2(i * 300, 400), Quaternion.identity, GameObject.FindGameObjectWithTag("Finish").transform);
                list.Add(obsj);

                log.AddEvent("Created Process : P" + (i - 1).ToString());
            }
            
        }

        // instantiate the X's queue
        var q = Instantiate(p, new Vector2(120, 300), Quaternion.identity, GameObject.FindGameObjectWithTag("Finish").transform);
        list1.Add(q);
        Text txt;
        txt = list1[0].GetComponentInChildren<Text>();

        char[] charArray = x.text.ToCharArray();
        Array.Reverse(charArray);
        string b = new string(charArray);
        txt.text = " " + b.ToString();

        //print formula
        genFromula();

        // writing into boxes the process number
        for (int i = 0; i < list.Capacity ; i++)
        {
            Text[] first;
            first = list[i].GetComponentsInChildren<Text>();
            first[2].text = arr[i].ToString();
            first[1].text = "P" + i.ToString();
        }
    }

    void genFromula()
    {
        TEXDraw aa;
        aa = formula.GetComponent<TEXDraw>();
        aa.text = "P(x) = " + arr[0].ToString();

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] < 0)
            {
                if (i <= arr.Length - 2)
                {
                    aa.text = aa.text + "*x^" + (arr.Length - lengthFormula).ToString() + " "+arr[i].ToString();
                    lengthFormula += 1;
                }
                else
                {
                    aa.text = aa.text + "*x" + " " + arr[i].ToString();
                    lengthFormula += 1;
                }
            }
            else
            {
                if (i <= arr.Length - 2)
                {
                    aa.text = aa.text + "*x^" + (arr.Length - lengthFormula).ToString() + "+" + arr[i].ToString();
                    lengthFormula += 1;
                }
                else
                {
                    aa.text = aa.text + "*x" + "+" + arr[i].ToString();
                    lengthFormula += 1;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
