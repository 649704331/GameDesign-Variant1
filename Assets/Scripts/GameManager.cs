using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject endUI;
    public GameObject deathUI;
    public GameObject normalwin;
    public GameObject chat;
    //public static GameManager Instance;

    public void Awake()
    {
        Data.isGame = true;
    }
    public void NormalWin()
    {
        normalwin.SetActive(true);
        Data.isGame = false;
    }

    public void RealWin()
    {
        chat.SetActive(true);
        Data.isGame = false;
    }

    public void Win()
    {
        endUI.SetActive(true);
        Data.isGame = false;
    }
    public void Failed()
    {
        deathUI.SetActive(true);
    }


    public void StartMenu()
    {
        endUI.SetActive(true);
        Data.isGame = false;
    }

    public void OnContinueButton()
    {
        endUI.SetActive(false);
        Data.isGame = true;
    }
    public void OnRestartButtonTeach()
    {
        Data.isGame = true;
        SceneManager.LoadScene("TeachScene");
    }
    public void OnRestartButtonOne()
    {
        Data.isGame = true;
        SceneManager.LoadScene("SceneOne");
    }

    public void OnRestartButtonTwo()
    {
        Data.isGame = true;
        SceneManager.LoadScene("SceneTwo");
    }
    public void OnRestartButtonThree()
    {
        Data.isGame = true;
        SceneManager.LoadScene("SceneThree");
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
