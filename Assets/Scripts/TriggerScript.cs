using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{
    public Scene scene;

    public GameObject MisssionPoint1;
    public GameObject MisssionPoint2;
    public GameObject MisssionPoint3;
    public GameObject MisssionPointBottom;
    public GameObject Player;
    public GameObject Dog;

    public string Tip0;
    public string Tip1;
    public string Tip2;
    public string Tip2_1;
    public string Tip3;

    public string MissionTip0;
    public string MissionTip1;
    public string MissionTip2;
    public string MissionTip2_1;
    public string MissionTip3;

    public string TipBottom;    
    public int state = 1;
    //第一关所需变量
    public int DeadDog;
    public bool bottomstate;


    public float time;
    //

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "SceneTwo")
        {
            GameObject.Find("GameLogic").GetComponent<SceneOneScript>().MisssionPoint.SetActive(true);
            MisssionPoint1 = GameObject.Find("Transpoint");
            GameObject.Find("GameLogic").GetComponent<SceneOneScript>().MisssionPoint.SetActive(false);
            MisssionPointBottom = GameObject.Find("PlaneBottom");
        }
        Tip0 ="操作指南：\nW键：向前走\nA键：向左走\nS键：向后走\nD键：向右走\n" + "\n鼠标移动可以控制视角\n鼠标滚轮可以控制视角缩放";
        MissionTip0 = "任务目标：移动至发光区域";
        Tip1 = "接下来你要移动到第二个发光点，注意这个发光点位于高处，你必须使用space键跃上高处才能到达\n\n操作技巧：靠近高台边缘后先按下space，之后在空中按前进键方可跳上高台";
        MissionTip1 = "任务目标：移动至高处的发光区域";
        Tip2 = "接下来你要试着击败你的第一个敌人了。单击鼠标左键可以做出挥砍动作，砍中敌人将会造成伤害。试着击败前方的怪物吧！\n\n操作技巧：靠近敌人时单击鼠标左键，砍中敌人即可造成伤害。";
        MissionTip2 = "任务目标：杀死目标怪物";
        Tip2_1 = "干得漂亮！你已经成功杀死目标怪物，现在移动到发光点即可通过教学关";
        MissionTip2_1 = "任务目标：移动至发光区域";

        Tip3 = "恭喜你，已经完成所有教程内容!将在3秒钟后进入下一关……";
        MissionTip3 = "教学关卡已完成";
        TipBottom = "你已经掉到低地，无法跃上，按下R键可以回到初始位置";
        bottomstate = false;
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();
        if(scene.name!="TeachScene"&& scene.name != "SceneThree")
            DeadDog = GameObject.Find("GameLogic").GetComponent<SceneOneScript>().DeadDog;
        if (Input.GetKeyDown(KeyCode.R)&&scene.name!= "SceneThree")
        {
            bottomstate = false;
            Player.transform.position = new Vector3(73.3F, 32.62F, 158.46F);
            if (scene.name == "TeachScene")
            {
                switch (state)
                {
                    case 1:
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip0);
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip0); break;
                    case 2:
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip1);
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                    case 3:
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip2);
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip2); break;
                    default: break;
                }
            }
        }
        
        //教学关触发事件
        if (scene.name == "TeachScene")
        {
            if (Dog.GetComponent<MonsterWander>().CheckDeath() == true && state == 3)
            {
                MisssionPoint3.SetActive(true);
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip2_1);
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip2_1);
            }
            //通关后教学提示信息
            if (state == 4 && time + 1f < Time.time)
            {
                Tip3 = "恭喜你，已经完成所有教程内容!将在2秒钟后进入下一关……";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
            }
            if (state == 4 && time + 2f < Time.time)
            {
                Tip3 = "恭喜你，已经完成所有教程内容!将在1秒钟后进入下一关……";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
            }
            if (state == 4 && time + 3f < Time.time)
                SceneManager.LoadScene("black1");
        }

        if (scene.name == "SceneOne")
        {
            //教学提示信息
            if(DeadDog<4&&bottomstate==false)
            {
                Tip0 = "这一关的任务是击败场景中的所有小怪，试着使用教学关学习的攻击技巧来通关吧！";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip0);
            }
            else if(DeadDog == 4 && bottomstate == false&&state==1)
            {
                Tip0 = "很好！你已经杀死了所有小怪，现在移动到发光处的传送点即可进入下一关。";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip0);
            }
            //任务提示信息
            switch (DeadDog)
            {
                case 0: MissionTip1 = "任务目标：杀死所有小怪（0/4）";
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                case 1: MissionTip1 = "任务目标：杀死所有小怪（1/4）";
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                case 2: MissionTip1 = "任务目标：杀死所有小怪（2/4）";
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                case 3: MissionTip1 = "任务目标：杀死所有小怪（3/4）";
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                case 4: if(state==1)
                    {
                        MissionTip1 = "任务目标：移动到传送点";
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1);
                    }MisssionPoint1.SetActive(true); break;
            }
            //通关后教学提示信息
            if (state == 2 && time + 1f < Time.time)
            {
                Tip3 = "恭喜你，已经通过第一关!将在2秒钟后进入下一关……";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
            }
            if (state == 2 && time + 2f < Time.time)
            {
                Tip3 = "恭喜你，已经通过第一关!将在1秒钟后进入下一关……";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
            }
            //通关后加载
            if (state == 2 && time + 3f < Time.time)
            {
                SceneManager.LoadScene("black2");
            }
        }

        //场景2
        if (scene.name == "SceneTwo")
        {
            if (DeadDog < 4 && bottomstate == false)
            {
                Tip0 = "这一关的任务是击败场景中的所有小怪，注意躲避地上的火焰陷阱！巧妙地利用切换形态来通过这一关吧！\n\n提示：按Q键可以切换至狼形态，狼形态下不能攻击，但是跳跃能力和防火能力提升";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip0);
            }
            else if (DeadDog == 4 && bottomstate == false && state == 1)
            {
                Tip0 = "很好！你已经杀死了所有小怪，现在移动到发光处的传送点即可进入下一关。\n\n注意：狼形态下无法传送";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip0);
            }
            switch (DeadDog)
            {
                case 0:
                    MissionTip1 = "任务目标：杀死所有小怪（0/4）";
                    GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                case 1:
                    MissionTip1 = "任务目标：杀死所有小怪（1/4）";
                    GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                case 2:
                    MissionTip1 = "任务目标：杀死所有小怪（2/4）";
                    GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                case 3:
                    MissionTip1 = "任务目标：杀死所有小怪（3/4）";
                    GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); break;
                case 4:
                    if (state == 1)
                    {
                        MissionTip1 = "任务目标：移动到传送点";
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1);
                    }
                    MisssionPoint1.SetActive(true); break;
            }
            Debug.Log(state);

            if (state == 2 && time + 1f < Time.time)
            {
                Tip3 = "恭喜你，已经通过第二关!将在2秒钟后进入下一关……";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
            }
            if (state == 2 && time + 2f < Time.time)
            {
                Tip3 = "恭喜你，已经通过第二关!将在1秒钟后进入下一关……";
                GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
            }
            if (state == 2 && time + 3f < Time.time)
            {
                Loading.nextSceneName = "SceneThree";
                SceneManager.LoadScene("Loading3");
            }
        }
        if (scene.name == "SceneThree")
        {
            Tip0 = "运用技巧击败父亲和琦，注意躲避地上的岩浆！\n\n提示：琦的嚎叫和父亲的火攻对人形态会产生巨额伤害";
            GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip0);
            MissionTip0 = "任务目标:击败父亲和琦";
            GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("进入触发");
        //Debug.Log(scene.name);
        
        //落到下面
        if (other.tag.CompareTo("Bottom") == 0)
        {
            Debug.Log(0);
            if(scene.name=="SceneTwo"|| scene.name == "SceneThree")
            {
                TipBottom = "你已经掉到低地，无法跃上，按下R键可以回到初始位置\n\n注意：狼形态下无法传送";
            }
            GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(TipBottom);
            bottomstate = true;
        }
        else
        {
            //教学关卡传送点的触发
            if (scene.name == "TeachScene")
            {
                Debug.Log(state);
                switch (state)
                {
                    case 1:
                        MisssionPoint1.SetActive(false); MisssionPoint2.SetActive(true);
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip1);
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip1); state++; break;
                    case 2:
                        MisssionPoint2.SetActive(false); //
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip2);
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip2); state++; break;
                    case 3:
                        MisssionPoint3.SetActive(false);
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
                        GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip3);
                        time = Time.time;
                        state++; break;
                    default: break;
                }
            }
            //第一关事件触发
            if (scene.name == "SceneOne")
            {
                if(state==1)
                {
                    state++;
                    Tip3 = "恭喜你，已经通过第一关!将在3秒钟后进入下一关……";
                    MissionTip3 = "第一关已完成";
                    //MisssionPoint3.SetActive(false);
                    GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
                    GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip3);                   
                    time = Time.time;
                }
            }
            if (scene.name == "SceneTwo"&& other.tag.CompareTo("Transport") == 0)
            {
                if (state == 1)
                {
                    state++;
                    Tip3 = "恭喜你，已经通过第二关!将在3秒钟后进入下一关……";
                    MissionTip3 = "第二关已完成";
                    //MisssionPoint3.SetActive(false);
                    GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo(Tip3);
                    GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeMissionInfo(MissionTip3);
                    time = Time.time;
                }
            }
        }           
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("离开触发");
        //GameObject.Find("GameLogic").GetComponent<ChangeTips>().ChangeInfo("离开触发器！");
        //Destroy(GetComponent<Collider>());
    }
}
