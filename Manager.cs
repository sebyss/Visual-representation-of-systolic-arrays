using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
    
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        InitiateScript.list = new List<GameObject>();
        InitiateScript.list1 = new List<GameObject>();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
