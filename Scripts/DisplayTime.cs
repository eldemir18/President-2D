using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI presidentTimeTMP;

    private int presidentTime;

    void Start()
    {
        presidentTime = PlayerPrefs.GetInt("PresidentTime");

        int years = presidentTime / 12;
        int months = presidentTime % 12;

        string yearString  = (years == 1)  ? " year" : " years";
        string monthString = (months == 1) ? " month" : " months";

        presidentTimeTMP.text = years + yearString + " " + months + monthString;
    }
}
