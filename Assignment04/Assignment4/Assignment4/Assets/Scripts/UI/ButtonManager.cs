using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public bool xAxis;
    public DataVisualizer dataVisualizer;
    public Button[] myButtons;

    public void ButtonPressed(string variableName)
    {
        foreach(Button b in myButtons)
        {
            b.interactable = true;
        }

        variableName = variableName.ToLower();

        if (variableName.Equals("hoursplayed"))
        {
            myButtons[0].interactable = false;
        }
        else if (variableName.Equals("level"))
        {
            myButtons[1].interactable = false;
        }
        else if (variableName.Equals("pelletseaten"))
        {
            myButtons[2].interactable = false;
        }
        else if (variableName.Equals("fruiteaten"))
        {
            myButtons[3].interactable = false;
        }
        else if (variableName.Equals("ghostseaten"))
        {
            myButtons[4].interactable = false;
        }
        else if (variableName.Equals("avgscore"))
        {
            myButtons[5].interactable = false;
        }
        else if (variableName.Equals("maxscore"))
        {
            myButtons[6].interactable = false;
        }
        else if (variableName.Equals("totalscore"))
        {
            myButtons[7].interactable = false;
        }

        if (xAxis)
        {
            dataVisualizer.variableX = variableName;
        }
        else
        {
            dataVisualizer.variableY = variableName;
        }
    }
}
