using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class My3rdCamera : MonoBehaviour
{
    public Transform Target;
    public float RotateSpeed, AngleLimitUp, AngleLimitDown, Height;

    private Transform FrontPos, BackPos, MainCamera;
    private float RotateX, RotateY;

    private bool warrior;

    public GameManager myGameManager;

    // Use this for initialization
    void Start()
    {
        Data.warrior_hp = 500;
        Data.wolf_hp = 1000;
        Data.warrior_max_hp = 500;
        Data.wolf_max_hp = 1000;
        Data.attackTime = 0;
        Data.isLive = true;
        warrior = true;
        MainCamera = GameObject.Find("Main Camera").transform;
        FrontPos = GameObject.Find("Front Position").transform;
        BackPos = GameObject.Find("Back Position").transform;
        transform.position = new Vector3(Target.position.x, Target.position.y + Height, Target.position.z);
        MainCamera.localPosition = BackPos.localPosition;
        MainCamera.rotation = BackPos.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 100)
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= 20)
                Camera.main.orthographicSize += 0.5F;
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 2)
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= 1)
                Camera.main.orthographicSize -= 0.5F;
        }

        if (Data.isLive && Data.isGame)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                myGameManager.GetComponent<GameManager>().StartMenu();
            }

            /* 切换视角 */
            Scene scene = SceneManager.GetActiveScene();
            //Debug.Log(scene.name);
            //切换形态，更改相机追随对象
            if (Input.GetKeyDown(KeyCode.Q) && scene.name != "TeachScene"&&scene.name != "SceneOne")
            {
                if (warrior == true)
                {
                    //从人形态切换为狼形态
                    changeWolf();
                    Target = GameObject.Find("Player_Wolf").transform;
                    warrior = false;
                    print("warrior to wolf");
                }
                else if (warrior == false)
                {
                    //从狼形态切换为人形态
                    changeWarrior();
                    Target = GameObject.Find("Player").transform;
                    warrior = true;
                    print("wolf to warrior");
                }
            }

            //第一视角切换
            transform.position = new Vector3(Target.position.x, Target.position.y + Height, Target.position.z);
            if (Input.GetKey(KeyCode.V))
            {
                MainCamera.localPosition = FrontPos.localPosition;
                MainCamera.rotation = FrontPos.rotation;
            }
            else
            {
                MainCamera.localPosition = BackPos.localPosition;
                MainCamera.rotation = BackPos.rotation;
            }

            //鼠标左右移动旋转视角
            RotateX = (RotateX + RotateSpeed * Input.GetAxis("Mouse X")) % 360f;
            RotateY = (RotateY - RotateSpeed * Input.GetAxis("Mouse Y")) % 360f;
            if (RotateY > AngleLimitUp)
                RotateY = AngleLimitUp;
            if (RotateY < AngleLimitDown)
                RotateY = AngleLimitDown;
            transform.rotation = Quaternion.AngleAxis(RotateX, Vector3.up) * Quaternion.AngleAxis(RotateY, Vector3.right);
        }
    }

    private void changeWarrior()
    {
        Vector3 a = Target.transform.position;
        Quaternion b = Target.rotation;
        GameObject new_Warrior = (GameObject)Resources.Load("Player");
        new_Warrior = Instantiate(new_Warrior, a, b);
        new_Warrior.name = "Player";
    }
    
    private void changeWolf()
    {
        Vector3 a = Target.transform.position;
        Quaternion b = Target.rotation;
        GameObject new_wolf = (GameObject)Resources.Load("Player_Wolf");
        new_wolf = Instantiate(new_wolf, a, b);
        new_wolf.name = "Player_Wolf";
    }
}
