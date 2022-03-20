using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSpeed : MonoBehaviour
{


    Text text;
    float increment;
    private void Start()
    {
        text = this.GetComponent<Text>();
        increment = 0;//初始为0
    }

    void Update()//每帧都会被执行
    {
        if (Input.GetMouseButtonDown(0))//如果点击了鼠标左键
        {
            increment = 0;
            Color color = text.color;
            color.a = increment;
            text.color = color;
        }
        if (text.color.a <= 1)//虽然外部显示的是255，但在脚本内alpha的从0到1的
        {
            /*颜色的变化*/
            increment += 0.5f * Time.deltaTime;//增量每帧都增加
            /*这里我也想直接写成text.color.a=increment，但无奈这东西是只读属性，只能如下写法*/
            Color color = text.color;
            color.a = increment;
            text.color = color;
        }
    }
}

