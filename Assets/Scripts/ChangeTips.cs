using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;
using System.Threading;

public class ChangeTips : MonoBehaviour
{
    // Start is called before the first frame update
    private string info;
    private string missioninfo;
    private bool LoopState = true;

    [SerializeField]public Text Info;
    [SerializeField]public Text MissionInfo;

    void Start()
    {
        ChangeMissionInfo("任务目标：移动至发光区域");
        ChangeInfo("游戏开始，请移动至发光区域");
        //Thread.Sleep(1000);
        ChangeInfo("操作指南：\nW键：向前走\nA键：向左走\nS键：向后走\nD键：向右走\n" + "\n鼠标移动可以控制视角\n鼠标滚轮可以控制视角缩放");

        //while (LoopState)
        //{ ChangeLoopState();
        //}
    }

    //改变文本框内容，str为要变成的内容
    public void ChangeLoopState()
    {
        LoopState =(LoopState == true) ?  false : LoopState = true;
    }

    public void ChangeInfo(string str)
    {
        info = str;
        Info = GameObject.Find("Canvas/Frame/Info").GetComponent<Text>();
        Info.text = info;
    }

    public void ChangeMissionInfo(string str)
    {
        missioninfo = str;
        MissionInfo = GameObject.Find("Canvas/MissionTip/MissionInfo").GetComponent<Text>();
        MissionInfo.text = missioninfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
