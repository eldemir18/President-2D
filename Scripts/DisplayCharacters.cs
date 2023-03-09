using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCharacters : MonoBehaviour
{
    [SerializeField] GameObject characterPrefab;
    [SerializeField] List<Characters> characters;

    void Start()
    {
        CreateCharacters();
    }

    private void CreateCharacters()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            GameObject newCharacter = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity);
            
            TextMeshProUGUI nameTMP = newCharacter.GetComponentInChildren<TextMeshProUGUI>();
            Image characterImage = newCharacter.GetComponentInChildren<Image>();

            if(PlayerPrefs.HasKey(characters[i].characterName))
            {
                nameTMP.text = characters[i].characterName;
                characterImage.GetComponentInChildren<Image>().sprite = characters[i].characterSprite;
            }
            else
            {
                nameTMP.text = "?????";
                characterImage.color = new Color(0.37f, 0.37f, 0.37f);
            }

            newCharacter.transform.SetParent(transform);
            newCharacter.transform.localScale = Vector3.one;
        }
    }
}
