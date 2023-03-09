using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    [TextArea(10,20)]
    public string eventDescription;
    
    [Header("Right Selection")]
    [TextArea(10,20)]
    public string rightDescription;
    public int rightPeople;
    public int rightMoney;
    public int rightArmy;

    [Header("Left Selection")]
    [TextArea(10,20)]
    public string leftDescription;
    public int leftPeople;
    public int leftMoney;
    public int leftArmy;

}
