using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBrother : MonoBehaviour
{
    private GameObject playerUnit;          //获取玩家单位
    private float boss_bro_hp = 1000f;
    private float boss_Max_bro_hp = 1000f;
    private bool isDeath = false;
    private bool isAttack = false;
    private bool isWarrior;
    private float distanceToPlayer;         //怪物与玩家的距离
    private Vector3 player_position;
    private Quaternion targetRotation;         //怪物的目标朝向
    public float turnSpeed;         //转身速度，建议0.1
    private Vector3 Vec;
    private bool isGeneralAttack;

    private bool isRun = true;

    //时间
    private float checkTime;
    private float proTime = 0.0f;
    private float nextTime = 0.0f;

    //UGUI
    public Slider healthSlider;
    private Animator targetAnim;
    private Animator myAnim;
    public GameManager myGameManager;
    // Start is called before the first frame update
    void Start()
    {
        isRun = true;
        checkTime = 0.5f;
        isDeath = false;
        isWarrior = true;
        boss_bro_hp = 1000f;
        boss_Max_bro_hp = 1000f;
        playerUnit = GameObject.FindGameObjectWithTag("Player");
        targetAnim = playerUnit.GetComponent<Animator>();
        distanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
        Vec = transform.position - playerUnit.transform.position;
        myAnim = GetComponent<Animator>();
        Data.isBroDeath = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Data.isLive && Data.isGame && isDeath == false && !Data.isDadDeath)
        {
            playerUnit = GameObject.FindGameObjectWithTag("Player");

            distanceToPlayer = Vector3.Distance(playerUnit.transform.position, transform.position);
            //作弊码
            if (Input.GetKeyDown(KeyCode.M))
            {
                boss_bro_hp = 0;
                healthSlider.value = boss_bro_hp;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (isWarrior)
                    isWarrior = false;
                else
                    isWarrior = true;
            }

            //死亡
            if (boss_bro_hp <= 0)
            {
                myAnim.SetTrigger("Death");
                isDeath = true;
                Data.isBroDeath = true;
            }

            //受到攻击
            if (isWarrior)
            {
                Vec = transform.position - playerUnit.transform.position;
                targetAnim = playerUnit.GetComponent<Animator>();
                if (distanceToPlayer <= 7 && targetAnim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack_1"))
                {
                    boss_bro_hp -= 30 * Time.deltaTime;
                    healthSlider.value = boss_bro_hp;
                }
            }

            //距离判定，远则追击，进则待机等待咬动条件
            if (distanceToPlayer >= 6)
            {
                //追击
                myAnim.SetFloat("Speed", 1f);
                //追击主角
                player_position = playerUnit.transform.position + new Vector3(3.5f, 0, 3.5f);
                //player_position.y = player_position.y - player_position.y + 33f;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, player_position, 0.1f);
                //朝向玩家位置
                Vector3 temp_Pos = playerUnit.transform.position - transform.position;
                temp_Pos.y = temp_Pos.y - temp_Pos.y;
                targetRotation = Quaternion.LookRotation(temp_Pos, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
            }
            else
            {
                myAnim.SetFloat("Speed", 0.2f);
                myAnim.SetTrigger("Bite");
                isGeneralAttack = true;

                //咬动技能攻击
                proTime = Time.fixedTime;
                if ((boss_bro_hp <= 900 && boss_bro_hp >= 899) || (boss_bro_hp <= 800 && boss_bro_hp >= 799) ||
                    (boss_bro_hp <= 700 && boss_bro_hp >= 699) || (boss_bro_hp <= 600 && boss_bro_hp >= 599) ||
                    (boss_bro_hp <= 500 && boss_bro_hp >= 499) || (boss_bro_hp <= 400 && boss_bro_hp >= 399) ||
                    (boss_bro_hp <= 200 && boss_bro_hp >= 199) || (boss_bro_hp <= 100 && boss_bro_hp >= 99))
                {
                    if (proTime - nextTime >= 0.5f)
                    {
                        isAttack = true;
                    }
                }

                if (isAttack)
                {
                    isRun = false;
                    myAnim.SetFloat("Speed", 0f);
                    if (proTime - nextTime >= 3f)
                    {
                        nextTime = proTime;
                        myAnim.SetTrigger("Howl");
                        myAnim.SetTrigger("Bite");
                        isAttack = false;
                    }
                }
            }
        }
        else if (Data.isDadDeath && !Data.isBroDeath)
        {
            //停止攻击，弹出对话 
            myGameManager.GetComponent<GameManager>().RealWin();

        }
    }
}
