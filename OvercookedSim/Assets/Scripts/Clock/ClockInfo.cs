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
}
