using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LogScript : MonoBehaviour {

    // Private VARS
    private List<string> Eventlog = new List<string>();
    private string guiText = " ";

    void OnGUI()
    {
        GUI.contentColor = Color.green;
        GUI.TextArea(new Rect(Screen.width / 3, Screen.height - (Screen.height), Screen.width / 3, Screen.height / 5), guiText, GUI.skin.textArea);
    }

    public void AddEvent(string eventString)
    {
        Eventlog.Add(eventString);
        guiText = "";

        foreach (string logEvent in Eventlog)
        {
            guiText += logEvent;
            guiText += "\n";
        }
    }
}
