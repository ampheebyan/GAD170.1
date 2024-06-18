using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class CanvasHandler : MonoBehaviour
{
    public TMP_InputField _inputField;
    public Scrollbar _sBar;

    public void AppendToField(string text, int outType, string customOutType = "")
    {
        var prefix = "";
        switch(outType)
        {
            case 0:
                prefix = "<b>[<color=green>GAME</color>]</b>";
                break;
            case 1:
                prefix = "<b>[<color=orange>LEVEL UP</color>]</b>";
                break;
            case 2:
                prefix = "<b>[<color=orange>XP</color>]</b>";
                break;
            case 3:
                prefix = "<b>[<color=red>PLAYER</color>]</b>";
                break;
            case 5:
                prefix = "<b>["+customOutType+"]</b>";
                break;
        }
        _inputField.text = _inputField.text + prefix + " " + text + Environment.NewLine; // Helper function to add to inputField freely.
        _sBar.value = 1;
    }

    public void ClearField()
    {
        _inputField.text = ""; // Helper function to empty the inputField.
    }

}
