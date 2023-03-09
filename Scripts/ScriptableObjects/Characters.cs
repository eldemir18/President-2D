using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Characters : ScriptableObject
{
    public string characterName;

    public Sprite characterSprite;
        
    public List<Event> events;
}
