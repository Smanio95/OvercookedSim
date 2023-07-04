using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ClockInfo", menuName ="ScriptableObjects/Clock/ClockInfo")]
public class ClockInfo : ScriptableObject
{
    public string splitStr = ":";
    public string opHoursTxt = "(%~%)";
    public float openingHour = 9;
    public float closingHour = 22;
    public int deltaTimeDivider = 5;
    public int restTimer = 4;
    public float resetTimer = 0.7f;
    public Color defaultHourColor;
    public Color lateHourColor;

    public string BuildOPHoursTxt()
    {
        bool isOpening = true;
        string copyTxt = "";
        (string hourStr, string centsStr) = BuildHourStrings((int)openingHour, (int)(openingHour % 1));

        foreach (char c in opHoursTxt)
        {
            if (c == '%')
            {
                if (isOpening)
                {
                    isOpening = false;
                }
                else
                {
                    (hourStr, centsStr) = BuildHourStrings((int)closingHour, (int)(closingHour % 1));
                }
                copyTxt += $"{hourStr}:{centsStr}";
            }
            else
            {
                copyTxt += c;
            }
        }

        return copyTxt;
    }

    public (string hourStr, string centsStr) BuildHourStrings(int hour, int cents)
    {
        string centsStr = cents < 10 ? $"0{cents}" : $"{cents}";

        string hourStr = hour < 10 ? $"0{hour}" : $"{hour}";

        return (hourStr, centsStr);
    }
}
