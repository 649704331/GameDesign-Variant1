using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseTeach()
    {
        Loading.nextSceneName = "TeachScene";

        SceneManager.LoadScene("Loading0");
    }

    public void ChooseOne()
    {
       // Loading.nextSceneName = "SceneOne";

        SceneManager.LoadScene("black1");
    }
    public void ChooseTwo()
    {
        //Loading.nextSceneName = "SceneTwo";

        SceneManager.LoadScene("black2");
    }
    public void ChooseThree()
    {
        Loading.nextSceneName = "SceneThree";

        SceneManager.LoadScene("Loading3");
    }

}
