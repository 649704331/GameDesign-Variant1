using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject aboutCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStart()
    {
        SceneManager.LoadScene("Choice");
    }

    public void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnAboutButton()
    {
        startCanvas.SetActive(false);
        aboutCanvas.SetActive(true);
    }

    public void OnReturnButton()
    {
        aboutCanvas.SetActive(false);
        startCanvas.SetActive(true);
    }
}
