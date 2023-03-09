using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [Header("President")]
    [SerializeField] TextMeshProUGUI presidentTimeTMP;
    private int presidentTime = 0;

    [Header("Characters")]
    [SerializeField] List<Characters> characters;
    [SerializeField] TextMeshProUGUI characterNameTMP;
    [SerializeField] Image characterImage;
    [Space]
    [SerializeField] List<Characters> endings;
    [SerializeField] TextMeshProUGUI eventTMP;
    private Characters currentCharacter;
    private Event currentEvent;
    
    [Header("Relations")]
    [SerializeField] Slider peopleSlider;
    [SerializeField] Slider moneySlider;
    [SerializeField] Slider armySlider;

    [Space]

    [SerializeField] Image peopleDirection;
    [SerializeField] Image moneyDirection;
    [SerializeField] Image armyDirection;
    [SerializeField] Sprite[] icons;

    private int peopleValue;
    private int moneyValue;
    private int armyValue;
    private float lerpDuration = 0.5f;
    private int sliderMaxValue = 10;
    
    [Header("Selection")]
    [SerializeField] Button rightButton;
    [SerializeField] TextMeshProUGUI rightTMP;
    [SerializeField] Button leftButton;
    [SerializeField] TextMeshProUGUI leftTMP;


    private bool isGameOver;

    void Start()
    {
        SetSliders();
        UpdatePresidentTime();
        ChooseRandomEvent();
    }

    private void SetSliders()
    {
        peopleValue = sliderMaxValue / 2;
        moneyValue = sliderMaxValue / 2;
        armyValue = sliderMaxValue / 2;

        peopleSlider.maxValue = sliderMaxValue;
        moneySlider.maxValue = sliderMaxValue;
        armySlider.maxValue = sliderMaxValue;

        peopleSlider.value = peopleValue;
        moneySlider.value = moneyValue;
        armySlider.value = armyValue;
    }

    public void ChooseRandomEvent()
    {
        while(true)
        {
            int randomCharacterIndex = UnityEngine.Random.Range(0, characters.Count);

            currentCharacter = characters[randomCharacterIndex];
            
            int randomEventIndex = UnityEngine.Random.Range(0, currentCharacter.events.Count);
            
            if(currentEvent != currentCharacter.events[randomEventIndex])
            {
                currentEvent = currentCharacter.events[randomEventIndex];
                break;
            }
        }

        PlayerPrefs.SetInt(currentCharacter.characterName, 1);

        // Display new event
        DisplayEvent();
    }


    private void DisplayEvent()
    {
        characterNameTMP.text = currentCharacter.characterName;
        characterImage.sprite = currentCharacter.characterSprite;
        
        eventTMP.text = currentEvent.eventDescription;
        rightTMP.text = currentEvent.rightDescription;
        leftTMP.text = currentEvent.leftDescription;
    }

    public void SelectEvent(int button)
    {
        // Load ending scene here
        if(isGameOver)
        {
            PlayerPrefs.SetInt("PresidentTime", presidentTime);

            SceneLoader.GameOver();
        }
        
        switch(button)
        {
            case 0:
                UpdateSliderValues(currentEvent.leftPeople, currentEvent.leftMoney, currentEvent.leftArmy);
                break;
            case 1:
                UpdateSliderValues(currentEvent.rightPeople, currentEvent.rightMoney, currentEvent.rightArmy);
                break;
            default:
                Debug.LogError("Direction out of bounds");
                return;
        }

        // ending
        if(IsGameOver())
        {
            isGameOver = true;
            DisplayEvent();
        }
        else
        {
            ChooseRandomEvent();
            UpdatePresidentTime();
        }
    }

    private bool IsGameOver()
    {
        if(peopleValue <= 0)
        {
            currentCharacter = endings[0];
        }
        else if(moneyValue <= 0)
        {
            currentCharacter = endings[1];
        }
        else if(armyValue <= 0)
        {
            currentCharacter = endings[2];
        }
        else if(peopleValue >= sliderMaxValue)
        {
            currentCharacter = endings[3];
        }
        else if(moneyValue >= sliderMaxValue)
        {
            currentCharacter = endings[4];

        }
        else if(armyValue >= sliderMaxValue)
        {
            currentCharacter = endings[5];
        }
        else
        {
            return false;
        }
        
        PlayerPrefs.SetInt(currentCharacter.characterName, 1);
        currentEvent = currentCharacter.events[0];

        return true;
    }

    private void UpdateSliderValues(int people, int money, int army)
    {
        peopleValue += people;
        moneyValue += money;
        armyValue += army;

        StartCoroutine(LerpSliderValue(peopleSlider, peopleValue, lerpDuration));
        StartCoroutine(LerpSliderValue(moneySlider, moneyValue, lerpDuration));
        StartCoroutine(LerpSliderValue(armySlider, armyValue, lerpDuration));
    }

    private IEnumerator LerpSliderValue(Slider slider, float targetValue, float duration)
    {
        float elapsedTime = 0f;
        float startValue = slider.value;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            slider.value = Mathf.Lerp(startValue, targetValue, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = targetValue;
    }

    public void UpdatePresidentTime()
    {
        presidentTime++;

        int years = presidentTime / 12;
        int months = presidentTime % 12;

        string yearString  = (years == 1)  ? " year" : " years";
        string monthString = (months == 1) ? " month" : " months";

        presidentTimeTMP.text = years + yearString + " " + months + monthString;
    }

    public void SetSliderDirections(int button)
    {
        switch(button)
        {
            case 0:
                SetImageSprites(currentEvent.leftPeople, currentEvent.leftMoney, currentEvent.leftArmy);
                break;
            case 1:
                SetImageSprites(currentEvent.rightPeople, currentEvent.rightMoney, currentEvent.rightArmy);
                break;
            default:
                Debug.LogError("Direction out of bounds");
                return;
        }
    }

    private void SetImageSprites(int people, int money, int army)
    {
        SetColorsOfImage(peopleDirection, 1f);
        SetColorsOfImage(moneyDirection, 1f);
        SetColorsOfImage(armyDirection, 1f);

        if (people <= -1)
        {
            peopleDirection.sprite = icons[0];
            peopleDirection.color = Color.red;
        }
        else if (people == 0)
        {
            SetColorsOfImage(peopleDirection, 0f);
        }
        else
        {
            peopleDirection.sprite = icons[1];
            peopleDirection.color = Color.green;
        }

        if (money <= -1)
        {
            moneyDirection.sprite = icons[0];
            moneyDirection.color = Color.red;
        }
        else if (money == 0)
        {
            SetColorsOfImage(moneyDirection, 0f);
        }
        else
        {
            moneyDirection.sprite = icons[1];
            moneyDirection.color = Color.green;
        }

        if (army <= -1)
        {
            armyDirection.sprite = icons[0];
            armyDirection.color = Color.red;
        }
        else if (army == 0)
        {
            SetColorsOfImage(armyDirection, 0f);
        }
        else
        {
            armyDirection.sprite = icons[1];
            armyDirection.color = Color.green;
        }
    }

    private void SetColorsOfImage(Image image, float a)
    {
        image.color = new Color(image.color.r,
                                image.color.g,
                                image.color.b,
                                a);
    }
}
