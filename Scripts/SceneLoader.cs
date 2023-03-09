using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    } 

    public static void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
