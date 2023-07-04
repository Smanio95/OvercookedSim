using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TMP_Text clockText;
    [SerializeField] TMP_Text clockOPHours;
    [SerializeField] Image fadePanel;
    [SerializeField] float fadePanelSpeed = 0.5f;
    [SerializeField] ClockInfo clockInfo;

    [HideInInspector] public bool isOpen = false;

    private float elapsed;

    public delegate void Closing();
    public static Closing OnClosing;

    private void Start()
    {
        elapsed = clockInfo.openingHour - clockInfo.resetTimer;

        BuildOPHoursTxt();

        OpenRestaurant();
    }

    private void Update()
    {
        CheckRestaurantOpening();

        BuildString((int)elapsed, (int)((elapsed % 1) * 100));
        elapsed += (Time.deltaTime / clockInfo.deltaTimeDivider);
    }

    void BuildString(int hour, int cents)
    {
        if (cents >= 60)
        {
            cents = 0;
            hour++;
            elapsed = hour;
        }

        hour %= 24;

        (string hourStr, string centsStr) = BuildHourStrings(hour, cents);

        clockText.text = hourStr + clockInfo.splitStr + centsStr;
    }

    void CheckRestaurantOpening()
    {
        if (!isOpen && elapsed >= clockInfo.openingHour)
        {
            isOpen = true;
        }

        if (isOpen && elapsed >= clockInfo.closingHour)
        {
            isOpen = false;
            clockText.color = clockInfo.lateHourColor;
        }
    }

    public void GoToSleep()
    {
        OnClosing?.Invoke();
        StartCoroutine(Fade(1));
    }
    public void OpenRestaurant() => StartCoroutine(Fade(0));

    IEnumerator Fade(float finalAlpha = 0)
    {
        Color currentColor = fadePanel.color;
        float currentAlpha = currentColor.a;

        while (currentAlpha != finalAlpha)
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, finalAlpha, Time.deltaTime * fadePanelSpeed);
            fadePanel.color = new(currentColor.r, currentColor.g, currentColor.b, currentAlpha);
            yield return null;
        }

        if(finalAlpha == 1)
        {
            yield return new WaitForSeconds(clockInfo.restTimer);
            clockText.color = clockInfo.defaultHourColor;
            Start();
        }

    }

    void BuildOPHoursTxt()
    {
        bool isOpening = true;
        string copyTxt = "";
        (string hourStr, string centsStr) = BuildHourStrings((int)clockInfo.openingHour, (int)(clockInfo.openingHour % 1));

        foreach (char c in clockInfo.opHoursTxt)
        {
            if(c == '%')
            {
                if (isOpening)
                {
                    isOpening = false;
                }
                else
                {
                    (hourStr,centsStr) = BuildHourStrings((int)clockInfo.closingHour, (int)(clockInfo.closingHour % 1));
                }
                copyTxt += $"{hourStr}:{centsStr}";
            }
            else
            {
                copyTxt += c;
            }
        }

        clockOPHours.text = copyTxt;
    }

    (string hourStr, string centsStr) BuildHourStrings(int hour, int cents)
    {
        string centsStr = cents < 10 ? $"0{cents}" : $"{cents}";

        string hourStr = hour < 10 ? $"0{hour}" : $"{hour}";

        return (hourStr, centsStr);
    }
}
