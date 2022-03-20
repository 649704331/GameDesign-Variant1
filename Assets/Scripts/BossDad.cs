using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDad : MonoBehaviour
{
    private Animator myAnim;
    private GameObject playerUnit;          //获取玩家单位
    private float boss_dad_hp = 1000f;
    private float boss_Max_dad_hp = 1000f;
    private bool isDeath = false;
    private bool isAttack = false;
    private bool isWarrior;
    private float distanceToPlayer;         //怪物与玩家的距离
    private Vector3 Vec;
    //UGUI
    public Slider healthSlider;
    private Animator targetAnim;
    public GameManager myGameManager;


    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        isAttack = false;
        isDeath = false;
        isWarrior = true;
        boss_dad_hp = 1000f;
        boss_Max_dad_hp = 1000f;
        playerUnit = GameObject.FindGameObjectWithTag("Player");
        targetAnim = playerUnit.GetComponent<Animator>();
        distanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        Vec = transform.position - playerUnit.transform.position;
        Data.isDadDeath = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Data.isLive && Data.isGame && isDeath == false)
        {
            playerUnit = GameObject.FindGameObjectWithTag("Player");

            distanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);

            //作弊码
            if (Input.GetKeyDown(KeyCode.N))
            {
                boss_dad_hp = 0;
                healthSlider.value = boss_dad_hp;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (isWarrior)
                    isWarrior = false;
                else
                    isWarrior = true;
            }

            //死亡
            //死亡
            if (boss_dad_hp <= 0)
            {
                myAnim.SetTrigger("Death");
                if (!Data.isBroDeath)
                {
                    myGameManager.GetComponent<GameManager>().RealWin();
                }
                else
                {
                    myGameManager.GetComponent<GameManager>().NormalWin();
                }
                Data.isDadDeath = true;
            }

            //受到攻击
            if (isWarrior)
            {
                Vec = transform.position - playerUnit.transform.position;
                targetAnim = playerUnit.GetComponent<Animator>();
                if (distanceToPlayer <= 7 && targetAnim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack_1"))
                {
                    boss_dad_hp -= 50 * Time.deltaTime;
                    healthSlider.value = boss_dad_hp;
                }
            }

            //地火攻击
            if ((boss_dad_hp <= 900 && boss_dad_hp >= 899) || (boss_dad_hp <= 800 && boss_dad_hp >= 799) ||
                (boss_dad_hp <= 700 && boss_dad_hp >= 699) || (boss_dad_hp <= 600 && boss_dad_hp >= 599) ||
                (boss_dad_hp <= 500 && boss_dad_hp >= 499) || (boss_dad_hp <= 400 && boss_dad_hp >= 399) ||
                (boss_dad_hp <= 200 && boss_dad_hp >= 199) || (boss_dad_hp <= 100 && boss_dad_hp >= 99))
            {
                isAttack = true;
            }

            //生成地火
            Vector3 a;
            Quaternion b;
            GameObject new_fire;
            if (isAttack)
            {
                a = playerUnit.transform.position;
                b = transform.rotation;
                new_fire = (GameObject)Resources.Load("Fire-ThreeLevel");
                new_fire = Instantiate(new_fire, a, b);
                new_fire.name = "Fire-ThreeLevel";
                isAttack = false;
            }
        }
    }
}
