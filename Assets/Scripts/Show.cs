using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour
{
    private float tempTime;
    //public Canvas canvas;
    public Image image;
    public Image textimage;
    public Image bottomimage;
    Color color1;
    Color color2;
    void Start()
    {
        color1 = textimage.color;
        color2 = bottomimage.color;
        //image = canvas.GetComponent<Image>();

        color1.a = 0;
        color2.a = 0;
        textimage.color = color1;
        bottomimage.color = color2;
        tempTime = Time.time;
        //tempTime = 0;
        //获取材质本来的属性  
        //canvas.GetComponent<Image>().material.color = new Color
        //(
        //        canvas.GetComponent<Image>().material.color.r,
        //        canvas.GetComponent<Image>().material.color.g,
        //        canvas.GetComponent<Image>().material.color.b,
        //需要改的就是这个属性：Alpha值  
        //        canvas.GetComponent<Image>().material.color.a
        //);
    }

    void Update()
    {
        
        if (Time.time < 2f+tempTime)
        {
            color1.a += 0.5f * Time.deltaTime;
            color2.a += 0.5f * Time.deltaTime;
        }

        if (Time.time > 4f + tempTime)
        {
            color1.a -= 0.5f * Time.deltaTime;
            color2.a -= 0.5f * Time.deltaTime;
        }
        
        textimage.color = color1;
        bottomimage.color = color2;
    }
}
