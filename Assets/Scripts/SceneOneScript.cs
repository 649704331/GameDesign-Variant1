using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOneScript : MonoBehaviour
{

    public GameObject Dog1;
    public GameObject Dog2;
    public GameObject Dog3;
    public GameObject Dog4;
    public GameObject MisssionPoint;
    private bool Dog1Die;
    private bool Dog2Die;
    private bool Dog3Die;
    private bool Dog4Die;

    public int DeadDog;

    // Start is called before the first frame update
    void Start()
    {
        Dog1Die = false;
        Dog2Die = false;
        Dog3Die = false;
        Dog4Die = false;
        DeadDog = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dog1.GetComponent<MonsterWander>().CheckDeath() == true&&Dog1Die==false)
        {
            DeadDog++;
            Dog1Die = true;
        }
        if (Dog2.GetComponent<MonsterWander>().CheckDeath() == true && Dog2Die == false)
        {
            DeadDog++;
            Dog2Die = true;
        }
        if (Dog3.GetComponent<MonsterWander>().CheckDeath() == true && Dog3Die == false)
        {
            DeadDog++;
            Dog3Die = true;
        }
        if (Dog4.GetComponent<MonsterWander>().CheckDeath() == true && Dog4Die == false)
        {
            DeadDog++;
            Dog4Die = true;
        }
    }
}
